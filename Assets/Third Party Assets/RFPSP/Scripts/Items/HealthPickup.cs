//HealthPickup.cs by Azuline Studios© All Rights Reserved
//script for health pickup items
using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
	private Transform myTransform;
	private FPSPlayer FPSPlayerComponent;
	
	[Tooltip("Amount of health this pickup should restore on use.")]
	public float healthToAdd = 25.0f;
	[Tooltip("True if this pickup should disappear when used/activated by player.")]
	public bool removeOnUse = true;
	[Tooltip("Sound to play when picking up this item.")]
	public AudioClip pickupSound;
	[Tooltip("Sound to play when health is full and item cannot be used.")]
	public AudioClip fullSound;
	[Tooltip("If not null, this texture used for the pick up crosshair of this item.")]
	public Sprite healthPickupReticle;
	
	void Start () {
		myTransform = transform;//manually set transform for efficiency
		FPSPlayerComponent = Camera.main.transform.GetComponent<CameraControl>().playerObj.GetComponent<FPSPlayer>();
		Physics.IgnoreCollision(myTransform.GetComponent<Collider>(), FPSPlayerComponent.FPSWalkerComponent.capsule, true);
	}
	
	public void PickUpItem (GameObject user){
		FPSPlayerComponent = user.GetComponent<FPSPlayer>();
	
		if (FPSPlayerComponent.hitPoints < FPSPlayerComponent.maximumHitPoints){
			//heal player
			FPSPlayerComponent.HealPlayer(healthToAdd);
			
			if(pickupSound){PlayAudioAtPos.PlayClipAt(pickupSound, myTransform.position, 0.75f);}
			
			if(removeOnUse){
				FreePooledObjects();
				//remove this pickup
				Object.Destroy(gameObject);
			}
			
		}else{
			//player is already at max health, just play beep sound effect
			if(fullSound){PlayAudioAtPos.PlayClipAt(fullSound, myTransform.position, 0.75f);}		
		}
	}
	
	//return pooled objects back to object pool to prevent them from being destroyed when this object is destroyed after use
	private void FreePooledObjects(){
		FadeOutDecals[] decals = gameObject.GetComponentsInChildren<FadeOutDecals>(true);
		foreach (FadeOutDecals dec in decals) {
			dec.parentObjTransform.parent = AzuObjectPool.instance.transform;
			dec.parentObj.SetActive(false);
		}
		//drop arrows if object is destroyed
		ArrowObject[] arrows = gameObject.GetComponentsInChildren<ArrowObject>(true);
		foreach (ArrowObject arr in arrows) {
			arr.transform.parent = null;
			arr.myRigidbody.isKinematic = false;
			arr.myBoxCol.isTrigger = false;
			arr.gameObject.tag = "Usable";
			arr.falling = true;
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			CustomCharacterController.instance.pickupHealthBtn.gameObject.SetActive(true);
			CustomCharacterController.instance.toDestroyPickup = gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
			CustomCharacterController.instance.pickupHealthBtn.gameObject.SetActive(false);
		}
    }

	float startTime;
	public IEnumerator DestroyWeapon(float waitTime)
	{
		startTime = Time.time;
		while (true)
		{
			if (startTime + waitTime < Time.time)
			{
				FreePooledObjects();
				CustomCharacterController.instance.pickupHealthBtn.gameObject.SetActive(false); // Set pickup button to false when pickup gun is destroyed
				Object.Destroy(gameObject);
				yield break;
			}
			yield return null;
		}
	}
	public void PickUpItem()
	{
		//	FPSPlayerComponent = user.GetComponent<FPSPlayer>();

		if (true)
		{
			//heal player
			FPSPlayerComponent.HealPlayer(healthToAdd);
			if (GameStat.instance.CollectedText)
			{
				GameStat.instance.CollectedText.text = "Health +" + healthToAdd;
				GameStat.instance.CollectedText.gameObject.SetActive(true);
			}
			if (pickupSound) { PlayAudioAtPos.PlayClipAt(pickupSound, myTransform.position, 0.75f); }

			if (removeOnUse)
			{
				FreePooledObjects();
				//remove this pickup
			}
			Object.Destroy(gameObject);

		}
		else
		{
			//player is already at max health, just play beep sound effect
			if (fullSound) { PlayAudioAtPos.PlayClipAt(fullSound, myTransform.position, 0.75f); }
		}
	}
}