using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemAttributes
{
    public Text itemName;
    public int itemIndex;
    public GameObject itemObject;
}


public class ItemSelectionManager : MonoBehaviour
{
    public GameObject gunMedicSelectionPanel;
    public GameObject[] selectedIndicator, newGunSpecs/*, LockImages*/;
    public GameObject[] Guns;
     //public GameObject[] LockImages;
    public static ItemSelectionManager instance;
    public Text cashTxt;
    
    public Button selectBtn;
    public Button purchaseBtn;
    public Button backBtn;
    //public int selectedItemNum = 0;
    int counter = 1;

    public GameObject SorryPanel;
    public Button closeSorryPanelBtn;

    public GameObject itemViewCamera;

    public Text itemNameTxt;
    public Text itemPriceTxt;

    public ItemAttributes[] itemsList;
    public GameObject RewardPanel2;

    public static int weaponSelectionCounter = 0;
    public AudioSource UnlockedSOund;
    public GameObject UnlockAllWeapons;

    #region GunStatsNew

    public Image damageBar, accuracyBar, aimBar, scopeBar, rangeBar;
    public Text damageText, accuracyText, aimText, scopeText, rangeText;

    public IEnumerator SetBarValue(Image barImg, float value, Text textField)
    {
        barImg.fillAmount = 0f;
        StopCoroutine(SetBarValue(barImg, value, textField));

        float temp = value / 100f;
        textField.text = value.ToString() + " %";
        while (barImg.fillAmount <= temp)
        {
            barImg.fillAmount += 0.03f;
            yield return new WaitForEndOfFrame();
        }
        StopCoroutine(SetBarValue(barImg, value, textField));
    }
    #endregion
    void PlayGunUnclockSound()
    {
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.SoundKey) == 1)
        {
            if (UnlockedSOund)
            {
                UnlockedSOund.Play();
            }
        }
    }

    private void Awake()
    {
        instance = this;
        selectBtn.onClick.AddListener(OnSelectBtnClick);
        purchaseBtn.onClick.AddListener(OnPurchaseBtnClick);
        backBtn.onClick.AddListener(OnBackBtnCLick);
    }
    private void OnEnable()
    {

        itemViewCamera.SetActive(true);
        
        //weaponSelectionCounter++;
        //if (weaponSelectionCounter >= 4 && GenericPopup.Instance)
        //{
        //    weaponSelectionCounter = 0;
        //    GenericPopup.Instance.OfferPopupActive("Unlock_All_Weapons");
       // }
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_UNLOCK_WEAPONS) == 1)
        {
            UnlockAllWeapons.SetActive(false);
        }
    }
    int selectedGun = 0;
    void Start()
    {
        SetWeaponsByDefault();
        InitializeValues();
        NewGunSelectON(selectedGun);
        
        //for (int i = 0; i < selectedIndicator.Length; i++)
        //{
        //    selectedIndicator[i].SetActive(false);
        //    newGunSpecs[i].SetActive(false);
        //}
        ////PlayerPrefs.SetInt("Selected_Gun", 6);
        ///*selectedGun = */
        //PlayerPrefs.SetInt("Selected_Gun", 1);

        //GameConfiguration.SetIntegerKeyValue("SMG3", 1);
        //selectedItemNum = GameConfiguration.GetIntegerKeyValue(GameConfiguration.SelectedItemKey);
        //print("this is default set Item Index : " + selectedItemNum);
        //InitItem();
        //for (int i = 0; i < Guns.Length; i++)
        //{
        //    Guns[i].SetActive(false);
        //}
        //Guns[selectedGun].SetActive(true);
        //selectedIndicator[selectedGun].SetActive(true);
        //itemNameTxt.text = "SMG3";
        //if (selectedGun == 0)
        //{
        //    selectBtn.gameObject.SetActive(true);
        //    purchaseBtn.gameObject.SetActive(false);
        //}
        //NewGunSelectON(selectedGun);
    }

    void SetWeaponsByDefault()
    {
        PlayerPrefs.SetInt("Selected_Gun", 1);
        for (int i = 0; i < GenericPopup.Instance.weaponsData.weaponsList.Length; i++)
        {
            if (GenericPopup.Instance.weaponsData.weaponsList[i].isBought)
            {
                PlayerPrefs.SetInt(GenericPopup.Instance.weaponsData.weaponsList[i].weaponName, 1);
            }
        }
    }
    void InitializeValues()
    {
        for (int i = 0; i < selectedIndicator.Length; i++)
        {
            selectedIndicator[i].SetActive(false);
            itemsList[i].itemName.text = GenericPopup.Instance.weaponsData.weaponsList[i].displayName;
        }
        InitItem();
        for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].SetActive(false);
        }
        Guns[selectedGun].SetActive(true);
        selectedIndicator[selectedGun].SetActive(true);
        itemNameTxt.text = GenericPopup.Instance.weaponsData.weaponsList[selectedGun].displayName;
    }
    void UnlockedGunsImages()
    {
        for (int i = 0; i < 10; i++)
        {
            MainMenuController.instance.lockImages[i].SetActive(false);
        }
    }
    public void InitItem()
    {
        for (int i = 0; i < itemsList.Length; i++)
        {
            if (counter == i)
            {
                itemNameTxt.text = GenericPopup.Instance.weaponsData.weaponsList[counter].displayName;
                itemPriceTxt.text = GenericPopup.Instance.weaponsData.weaponsList[counter].priceCoins.ToString();
                itemsList[i].itemObject.SetActive(true);
                if (IsWeaponBought())
                {
                    selectBtn.gameObject.SetActive(true);
                    purchaseBtn.gameObject.SetActive(false);
                   // LockImages[i].SetActive(false);
                }
                else
                {
                    purchaseBtn.gameObject.SetActive(true);
                    selectBtn.gameObject.SetActive(false);
                        // LockImages[i].SetActive(true);

                    }
               }
            else
            {
                itemsList[i].itemObject.SetActive(false);
            }
        }
    }
    bool IsWeaponBought()
    {
        if (PlayerPrefs.GetInt(GenericPopup.Instance.weaponsData.weaponsList[counter].weaponName, 0) == 1)
        {
            return true;
        }
        return false;
    }
    public void OnPurchaseBtnClick()
    {
        playBtnSound();
        int cashVal = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);

        if (GenericPopup.Instance.weaponsData.weaponsList[counter].priceCoins <= cashVal)
        {
            
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, cashVal - GenericPopup.Instance.weaponsData.weaponsList[counter].priceCoins);
            GameConfiguration.SetIntegerKeyValue(GenericPopup.Instance.weaponsData.weaponsList[counter].weaponName, 1);
            InitItem();
            //itemsList[counter].itemObject.SetActive(false);
            PlayGunUnclockSound();
        }
        else
        {
            GameConfiguration.SetIntegerKeyValue(GenericPopup.Instance.weaponsData.weaponsList[counter].weaponName, 0);
            //TopBarGameplay.Instance.CashPlusBtnClick();
        }
    }

    public void OnSelectBtnClick()
    {
        if (GameConfiguration.GetIntegerKeyValue(GenericPopup.Instance.weaponsData.weaponsList[counter].weaponName) == 1)
        {
            print("Item Selected : " + GenericPopup.Instance.weaponsData.weaponsList[counter].weaponName);
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.SelectedItemKey, itemsList[counter].itemIndex);
        }
        itemViewCamera.SetActive(false);
        if (MainMenuController.instance)
        {
            MainMenuController.instance.OnButtonClickSound();
            gunMedicSelectionPanel.SetActive(true);
            MainMenuController.instance.ItemSelectionPanel.SetActive(false);
        }
        

        

    }
    public void UnlockAllGunsSuccess()
    {
        GameConfiguration.UnlockAllGunsCallBack();
        UnlockedGunsImages();
    }
    public void OnBackBtnCLick()
    {
        if (MainMenuController.instance)
        {
            MainMenuController.instance.OnButtonClickSound(true);
            MainMenuController.instance.ItemSelectionPanel.SetActive(false);

           

            GameConfiguration.SetIntegerKeyValue(GameConfiguration.IsMainMenu, 0);
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.IsItemSelection, 1);

            if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
            {
                MainMenuController.instance.ShowLevelSelection(true);
            }
            else
            {
                MainMenuController.instance.modeSelectionPanel.SetActive(true);
            }
        }
    }


    public void ShowSorryPanel()
    {
        itemViewCamera.SetActive(false);
        SorryPanel.SetActive(true);
    }
    public void CloseSorryPanel()
    {
        playBtnSound();
        SorryPanel.SetActive(false);
        itemViewCamera.SetActive(true);
    }
    public void OnNextItemClick()
    {
        counter++;
        playBtnSound();
        if (counter == itemsList.Length)
        {
            counter = 0;
        }
        InitItem();
    }
    public void OnPrevItemClick()
    {
        counter--;
        playBtnSound();
        if (counter < 0)
        {
            counter = itemsList.Length - 1;
        }
        InitItem();
    }

    public void NewGunSelectON(int num)
    {
        counter = num;
        playBtnSound();
        for (int i = 0; i < selectedIndicator.Length; i++)
        {
            selectedIndicator[i].SetActive(false);
        }
        selectedIndicator[num].SetActive(true);
        InitItem();
        //Adeel coded this
        StartCoroutine(SetBarValue(damageBar, GenericPopup.Instance.weaponsData.weaponsList[num].damageValue, damageText));
        StartCoroutine(SetBarValue(aimBar, GenericPopup.Instance.weaponsData.weaponsList[num].aimValue, aimText));
        StartCoroutine(SetBarValue(accuracyBar, GenericPopup.Instance.weaponsData.weaponsList[num].accuracyValue, accuracyText));
        StartCoroutine(SetBarValue(scopeBar, GenericPopup.Instance.weaponsData.weaponsList[num].scopeValue, scopeText));
        StartCoroutine(SetBarValue(rangeBar, GenericPopup.Instance.weaponsData.weaponsList[num].rangeValue, rangeText));
    }
    public void SetGunNumbersAccordingToRFPS(int num)
    {
        PlayerPrefs.SetInt("Selected_Gun", num);
    }

    public void playBtnSound()
    {
        if (MainMenuController.instance)
        {
            MainMenuController.instance.OnButtonClickSound();
        }
    }

    //Gun Selection Reward
    public void OnWatchVideoBtnClick2()
    {

        int currentCash = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
        

    }

    void AwardPanelFalse2()
    {
        RewardPanel2.SetActive(false);
        itemViewCamera.SetActive(true);
    }
}
