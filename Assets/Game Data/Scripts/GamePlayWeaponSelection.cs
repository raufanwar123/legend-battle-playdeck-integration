using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GVNativeIAP;


public class GamePlayWeaponSelection : MonoBehaviour
{
    public static GamePlayWeaponSelection instance;
    public WeaponsData weaponsData;
    public WeaponButtons[] weaponBtns;
    GVIAPButton gVIAPButton;
    [System.Serializable]
    public class WeaponButtons
    {
        public Button mainButton;
        public Text gunNameText;
        public GameObject buyBtn;
        public Text price;
    }
    private void OnEnable()
    {
        CheckWeapons();
      //  SetInAppPurchaseFields();
    }
    void Awake ()
    {
        SingleTon();
    }
    void SingleTon()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void CheckWeapons()
    {
        for (int i = 0; i < weaponBtns.Length; i++)
        {

            weaponBtns[i].gunNameText.text = weaponsData.weaponsList[i].weaponName;
            if (PlayerPrefs.GetInt(weaponsData.weaponsList[i].weaponName) == 1)
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
            GenericPopup.Instance.SetMessageText("Success", weaponsData.weaponsList[id].displayName/*, GenericPopup.RewardType.UnlockSingleWeapon*/);
        SetInAppAnalytics(weaponsData.weaponsList[id].displayName);
        CheckWeapons();
    }
    void SetInAppAnalytics(string productSKU)
    {
        
    }
}
