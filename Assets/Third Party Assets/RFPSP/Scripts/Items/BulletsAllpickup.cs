using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsAllpickup : MonoBehaviour
{
    private GameObject weaponObj;//the GameObject that is a child of FPS Weapons which has the WeaponBehavior script attatched
    private WeaponBehavior WeaponBehaviorComponent;
    private PlayerWeapons PlayerWeaponsComponent;

    private Transform myTransform;

    [Tooltip("The number that corresponds with this weapon's index in the PlayerWeapons script weaponOrder array.")]
    public int weaponNumber = 0;
    [Tooltip("If this is a pooled object, set this number to its index in the object pool (to return object like an arrow back to pool after use).")]
    public int objectPoolIndex = 0;
    [Tooltip("True if this pickup should disappear when used/activated by player.")]
    public bool removeOnUse = true;

    [Tooltip("Sound to play when picking up this item.")]
    public AudioClip pickupSound;
    [Tooltip("Sound to play when ammo is full and item cannot be used.")]
    public AudioClip fullSound;

    [Tooltip("Amount of ammo to add when picking up this ammo item.")]
    public int ammoToAdd = 1;
    [Tooltip("If not null, this texture used for the pick up crosshair of this item.")]
    public Sprite ammoPickupReticle;


    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;//manually set transform for efficiency
        weaponObj = Camera.main.transform.GetComponent<CameraControl>().weaponObj;
        //find the PlayerWeapons script in the FPS Prefab to access weaponOrder array
        PlayerWeaponsComponent = weaponObj.GetComponentInChildren<PlayerWeapons>();

        // WeaponBehaviorComponent = weaponObj.GetComponent<WeaponBehavior>();
        //  Physics.IgnoreCollision(myTransform.GetComponent<Collider>(), PlayerWeaponsComponent.FPSPlayerComponent.FPSWalkerComponent.capsule, true);
        Invoke("DestroyBullet", 6); //junaid added this
    }


    public void DestroyBullet()
    {
        FreePooledObjects();
        Object.Destroy(gameObject);
    }
    public void PickUpItem()
    {
      
          //  PlayerWeaponsComponent.CurrentWeaponBehaviorComponent.maxAmmo += ammoToAdd;        
  //if (pickupSound) { PlayAudioAtPos.PlayClipAt(pickupSound, myTransform.position, 0.75f); }

        WeaponBehaviorComponent = PlayerWeaponsComponent.CurrentWeaponBehaviorComponent;

        if (true)
        {

            if (WeaponBehaviorComponent.ammo + ammoToAdd > WeaponBehaviorComponent.maxAmmo)
            {
                //just give player max ammo if they only are a few bullets away from having max ammo
                WeaponBehaviorComponent.ammo = WeaponBehaviorComponent.maxAmmo;
                if (GameStat.instance.CollectedText)   //Junaid will added condition here for multiplayer and single player
                {
                  GameStat.instance.CollectedText.text = "Max Ammo";
                   GameStat.instance.CollectedText.gameObject.SetActive(true);
                }
            }
            else
            {
                //give player the ammoToAdd amount
                WeaponBehaviorComponent.ammo += ammoToAdd;
                if (GameStat.instance.CollectedText)   //Junaid will added condition here for multiplayer and single player
                {
                    GameStat.instance.CollectedText.text = "Ammo Added+" + ammoToAdd;
                   GameStat.instance.CollectedText.gameObject.SetActive(true);
                }
            }

            //play pickup sound
            if (pickupSound)
            {
                PlayAudioAtPos.PlayClipAt(pickupSound, myTransform.position, 0.75f);
            }

            //equip/activate weapon if we picked up ammo for disposable, one-shot weapon like grenades
            if (!WeaponBehaviorComponent.doReload && !WeaponBehaviorComponent.haveWeapon && !WeaponBehaviorComponent.nonReloadWeapon)
            {
                WeaponBehaviorComponent.haveWeapon = true;
                PlayerWeaponsComponent.StartCoroutine(PlayerWeaponsComponent.SelectWeapon(WeaponBehaviorComponent.weaponNumber));
            }

            Object.Destroy(gameObject);
            if (removeOnUse)
            {
                FreePooledObjects();
                if (objectPoolIndex == 0)
                {
                    //remove this weapon pickup
                }
                else
                {
                    AzuObjectPool.instance.RecyclePooledObj(objectPoolIndex, myTransform.gameObject);
                }
            }
        }
        else
        {
            //if player is at max ammo, just play beep sound
            if (fullSound)
            {
                PlayAudioAtPos.PlayClipAt(fullSound, myTransform.position, 0.75f);
            }
        }

    }

    //return pooled objects back to object pool to prevent them from being destroyed when this object is destroyed after use
    private void FreePooledObjects()
    {
        FadeOutDecals[] decals = gameObject.GetComponentsInChildren<FadeOutDecals>(true);
        foreach (FadeOutDecals dec in decals)
        {
            dec.parentObjTransform.parent = AzuObjectPool.instance.transform;
            dec.parentObj.SetActive(false);
        }
        //drop arrows if object is destroyed
        ArrowObject[] arrows = gameObject.GetComponentsInChildren<ArrowObject>(true);
        foreach (ArrowObject arr in arrows)
        {
            arr.transform.parent = null;
            arr.myRigidbody.isKinematic = false;
            arr.myBoxCol.isTrigger = false;
            arr.gameObject.tag = "Usable";
            arr.falling = true;
        }
    }
}
