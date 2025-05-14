using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoins : MonoBehaviour
{

    private Transform myTransform;
    private FPSPlayer FPSPlayerComponent;

    [Tooltip("Amount of health this pickup should restore on use.")]
    public int CoinstoAdd = 50;
    [Tooltip("True if this pickup should disappear when used/activated by player.")]
    public bool removeOnUse = true;
    [Tooltip("Sound to play when picking up this item.")]
    public AudioClip pickupSound;
    [Tooltip("Sound to play when health is full and item cannot be used.")]
    public AudioClip fullSound;
    [Tooltip("If not null, this texture used for the pick up crosshair of this item.")]
    public Sprite CoinsPickupReticle;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;//manually set transform for efficiency
        FPSPlayerComponent = Camera.main.transform.GetComponent<CameraControl>().playerObj.GetComponent<FPSPlayer>();
        //     Physics.IgnoreCollision(myTransform.GetComponent<Collider>(), FPSPlayerComponent.FPSWalkerComponent.capsule, true);

        Invoke("DestroyCoin", 6); //junaid added this
    }
    public void DestroyCoin()
    {
        FreePooledObjects();
        Object.Destroy(gameObject);
  
    }
    public void PickUpItem()
    {
       // FPSPlayerComponent = user.GetComponent<FPSPlayer>();

       // if (FPSPlayerComponent.hitPoints < FPSPlayerComponent.maximumHitPoints)
       // {
            //heal player
            // FPSPlayerComponent.HealPlayer(healthToAdd);
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey,(GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey)+ CoinstoAdd));
        MyNPCWaveManager.instance.CoinsCollected++;  // junaid change this to multiplayerNPC manager  MyNPCWaveManager.instance.CoinsCollected++;
        if (GameStat.instance.CollectedText)  // junaid change this to MultiplayerGameStat
        {
           GameStat.instance.CollectedText.text = "Coin Added+" + 1; // junaid change this to MultiplayerGameStat
           GameStat.instance.CollectedText.gameObject.SetActive(true); // junaid change this to MultiplayerGameStat
        }

        Object.Destroy(gameObject);
            if (pickupSound) { PlayAudioAtPos.PlayClipAt(pickupSound, myTransform.position, 0.75f); }

            if (removeOnUse)
            {
                FreePooledObjects();
                //remove this pickup
            }

       // }
     //   else
       // {
         //   //player is already at max health, just play beep sound effect
           // if (fullSound) { PlayAudioAtPos.PlayClipAt(fullSound, myTransform.position, 0.75f); }
        //}
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
