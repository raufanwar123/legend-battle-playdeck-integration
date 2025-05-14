using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GamePlayInApps : MonoBehaviour
{
    public Button[] unlockWeaponsButton;
    public Button[] removeAdsButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void UnlockWeapons()
    {
        GameConfiguration.UnlockAllGunsCallBack();
        //EnableUnlockWeaponButtons(false);
        //PlayerPrefs.SetInt()
    }

    public void RemoveAds()
    {
        GameConfiguration.setRemoveAds();
    }

    public void EnableUnlockWeaponButtons(bool state)
    {

        foreach (Button b in unlockWeaponsButton)
        {
            if (b)
                b.interactable = state;
        }
    }


}
