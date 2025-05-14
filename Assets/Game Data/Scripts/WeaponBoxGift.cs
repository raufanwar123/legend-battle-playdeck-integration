using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GVNativeIAP;
using SubjectNerd.Utilities;
public class WeaponBoxGift : MonoBehaviour
{

    [System.Serializable]
    public class WeaponButtons
    {
        public Button mainButton;
        public GameObject buyBtn;
        public bool isUnlockForBox;
    }

    public PlayerWeapons playerWeapons;
    public WeaponsData weaponsData;
    [Reorderable]
    public WeaponButtons[] weaponBtns;
    GVIAPButton gVIAPButton;



    private void OnEnable()
    {
        CheckWeapons();
       // SetInAppPurchaseFields();
    }


    public void SelectWeapon(int index)
    {
        playerWeapons.SelectWeaponBySam(index);
        gameObject.SetActive(false);
        if (GameStat.instance)
        {
            GameStat.instance.GiftBoxesState(false);
        }
    }


    public void CheckWeapons()
    {
        for (int i = 0; i < weaponBtns.Length; i++)
        {

            // weaponBtns[i].gunNameText.text = weaponsData.weaponsList[i].weaponName;
            if (PlayerPrefs.GetInt(weaponsData.weaponsList[i].weaponName) == 1 || weaponBtns[i].isUnlockForBox)
            {
                weaponBtns[i].buyBtn.gameObject.SetActive(false);
                weaponBtns[i].mainButton.interactable = true;
            }
            else
            {
                weaponBtns[i].buyBtn.gameObject.SetActive(true);
                weaponBtns[i].mainButton.interactable = false;
            }
        }
    }

    void SetInAppPurchaseFields()
    {
        for (int i = 0; i < weaponBtns.Length; i++)
        {
            gVIAPButton = weaponBtns[i].buyBtn.GetComponent<GVIAPButton>();
            if (gVIAPButton)
                gVIAPButton.productId = weaponsData.weaponsList[i].weaponSku;
        }
    }

    public void WeaponPurchased(int id)
    {
        PlayerPrefs.SetInt(weaponsData.weaponsList[id].weaponName, 1);
        weaponBtns[id].buyBtn.gameObject.SetActive(false);
        if (GenericPopup.Instance)
            GenericPopup.Instance.SetMessageText("Success", weaponsData.weaponsList[id].displayName);
        SetInAppAnalytics(weaponsData.weaponsList[id].displayName);
        CheckWeapons();
    }

    void SetInAppAnalytics(string productSKU)
    {
        
    }
}
