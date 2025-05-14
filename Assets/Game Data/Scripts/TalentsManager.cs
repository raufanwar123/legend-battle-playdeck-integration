using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalentsManager : MonoBehaviour
{
    [Serializable]
    public class ItemsList
    {
        public string itemsUnlockedNames;
        public string itemsPopupName;
        public string itemsUnlockedMsg;
        public Text itemsUnlockedText;
        public Text itemsLvlText;
        //public int[] itemsLvlNumber;
        public int[] itemsLvlValue;
        public int itemsLvlNumber;
        public int[] itemsCombatLvl;
        public Image highlightImages;
        public Image itemsLockedImages;
        public Sprite itemsUnlockedSprite;
    }
    public ItemsList[] itemsList;

    int randomNumber;
    int currentCash;
    [HideInInspector]
    public static int upgradePriceIndex;

    [HideInInspector]
    public static int healtheCounter;
    [HideInInspector]
    public static int coinsCounter;
    [HideInInspector]
    public static int bulletsCounter;
    [HideInInspector]
    public static int reloadCounter;
    [HideInInspector]
    public static int fireCounter;
    [HideInInspector]
    public static int powerCounter;

    [HideInInspector]
    public static int combatValue;



    [Header("Upgrade Item")]
    public int[] upgradePrices;
    public Text upgradePriceText;
    public Button upgradeBtn;
    public Text mainCombatValue;
    public Text upgradeValue;
    public Text _TextCoinsTotal;


    [Header("Unlocked Popup Item")]
    public GameObject unlockedPopup;
    public Text popupTitle;
    public Text popupMsg;
    public Text popupItem;
    public Text popupItemCuurentValue;
    public Text popupItemNextValue;
    public Text popupCurrentCombatValue;
    public Text popupNextCombatValue;
    public Text popupCurrentLvl;
    public Text popupNextLvl;

    public Image popupUnlock;
    public AudioSource audioSource;
    public AudioClip audioClip;
    void OnEnable()
    {
        
        CheckIntialInfo();
        UpdateCash();
    }
    void CheckIntialInfo()
    {
        upgradePriceIndex = PlayerPrefs.GetInt("PriceIndex");
        Debug.Log("Upgrade Price : " + upgradePriceIndex);
        if (upgradePriceIndex > upgradePrices.Length)
        {
            upgradePriceText.text = "Full";
            upgradeBtn.interactable = false;
        }
        for (int i = 0; i < itemsList.Length; i++)
        {
            if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + i)==1)
            {
                itemsList[i].itemsLockedImages.gameObject.SetActive(false);
                itemsList[i].itemsUnlockedText.text = itemsList[i].itemsUnlockedNames;
                itemsList[i].itemsLvlText.text = "Lvl " + GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + i + GameConfiguration.KEY_ITEM_LVL_NUMBER).ToString();
                Debug.Log("Unlocked Items : " + GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + i));
            }
        }
        upgradePriceText.text = upgradePrices[upgradePriceIndex].ToString();
        upgradeValue.text = "Upgraded  " + upgradePriceIndex.ToString() + "  times";
        combatValue =PlayerPrefs.GetInt("CombatValue");
        mainCombatValue.text = combatValue.ToString();
        Debug.Log("Combat Value  : " + combatValue);
    }
    public void UpgradeBtnClick()
    {
        currentCash = GameConfiguration.getTotalCash();
        if (currentCash >= upgradePrices[upgradePriceIndex])
        {
            if (upgradePriceIndex < upgradePrices.Length)
            {
                upgradeBtn.interactable = false;
                StartCoroutine(UnlockOrUpgradeItems());
            }
            else
            {
                upgradePriceText.text = "Full";
                upgradeBtn.interactable = false;
            }
        }
        else
        {
            GenericPopup.Instance.SetMessageText("Failed", "You have Not Enough Coins for Upgarde");
        }

        MainMenuController.instance.OnButtonClickSound();

       // GoogleAllAds.Instance.ShowInterstitialAd();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UnlockedPopupDisable();
        }
    }
    void UpdateCash()
    {
        _TextCoinsTotal.text = GameConfiguration.getTotalCash().ToString();
    }
    IEnumerator UnlockOrUpgradeItems()
    {
        for (int i = 0; i < 19; i++)
        {
            randomNumber = UnityEngine.Random.Range(0, itemsList.Length);
            itemsList[randomNumber].highlightImages.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            for (int j = 0; j < itemsList.Length; j++)
            {
                itemsList[j].highlightImages.gameObject.SetActive(false);
            }
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        currentCash = GameConfiguration.getTotalCash();
        if (currentCash >= upgradePrices[upgradePriceIndex])
        {
            currentCash = currentCash - upgradePrices[upgradePriceIndex];
            GameConfiguration.setTotalCash(currentCash);
            UpdateCash();
            CheckUnlockItems(randomNumber);
             upgradePriceIndex++;
            PlayerPrefs.SetInt("PriceIndex", upgradePriceIndex);
            if (upgradePriceIndex < upgradePrices.Length)
            {
                upgradePriceText.text = upgradePrices[upgradePriceIndex].ToString();
                upgradeValue.text = "Upgraded  " + upgradePriceIndex.ToString() + "  times";
                upgradeBtn.interactable = true;
            }
            else
            {
                upgradePriceText.text = "Full";
                upgradeBtn.interactable = false;
            }
            UnlockedPopupEnable();
        }
        else
        {
            GenericPopup.Instance.SetMessageText("Failed", "You have Not Enough Coins for Upgarde");
        }
    }

   
    void CheckUnlockItems(int index)
    {
        switch (index)
        {
            case 0:
                if (healtheCounter <= 3)
                {
                    popupItemCuurentValue.text = "+ " + itemsList[index].itemsLvlValue[healtheCounter] + "%";
                    popupCurrentCombatValue.text = itemsList[index].itemsCombatLvl[healtheCounter].ToString();
                    combatValue = PlayerPrefs.GetInt("CombatValue");
                    combatValue = combatValue + itemsList[index].itemsCombatLvl[healtheCounter];
                   
                    PlayerPrefs.SetInt("CombatValue",combatValue);
                    mainCombatValue.text = combatValue.ToString();
                    healtheCounter++;
                    popupNextCombatValue.text = itemsList[index].itemsCombatLvl[healtheCounter].ToString();
                    popupCurrentLvl.text = healtheCounter.ToString();
                    popupItemNextValue.text = itemsList[index].itemsLvlValue[healtheCounter].ToString();
                    popupNextLvl.text = (healtheCounter + 1).ToString();
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index, 1);
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index + GameConfiguration.KEY_ITEM_LVL_NUMBER, healtheCounter);
                    itemsList[index].itemsLvlText.text = "Lvl " + healtheCounter;
                    itemsList[index].itemsLockedImages.gameObject.SetActive(false);
                    itemsList[index].itemsUnlockedText.text = itemsList[index].itemsUnlockedNames;

                    
                }
                
                
                break;
            case 1:
                if (coinsCounter <= 3)
                {
                    popupItemCuurentValue.text = "+ " + itemsList[index].itemsLvlValue[coinsCounter] + "%";
                    popupCurrentCombatValue.text = itemsList[index].itemsCombatLvl[coinsCounter].ToString();
                    combatValue = PlayerPrefs.GetInt("CombatValue");
                    combatValue = combatValue + itemsList[index].itemsCombatLvl[coinsCounter];

                    PlayerPrefs.SetInt("CombatValue", combatValue);
                    mainCombatValue.text = combatValue.ToString();
                    coinsCounter++;

                    popupNextCombatValue.text = itemsList[index].itemsCombatLvl[coinsCounter].ToString();
                    popupCurrentLvl.text = coinsCounter.ToString();
                    popupItemNextValue.text = itemsList[index].itemsLvlValue[coinsCounter].ToString();
                    popupNextLvl.text = (coinsCounter + 1).ToString();
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index, 1);
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index + GameConfiguration.KEY_ITEM_LVL_NUMBER, coinsCounter);
                    itemsList[index].itemsLvlText.text = "Lvl " + coinsCounter;
                    itemsList[index].itemsLockedImages.gameObject.SetActive(false);
                    itemsList[index].itemsUnlockedText.text = itemsList[index].itemsUnlockedNames;

                    
                }
               
                break;
            case 2:
                if (bulletsCounter <= 3)
                {
                    popupItemCuurentValue.text = "+ " + itemsList[index].itemsLvlValue[bulletsCounter] + "%";
                    popupCurrentCombatValue.text = itemsList[index].itemsCombatLvl[bulletsCounter].ToString();
                    combatValue = PlayerPrefs.GetInt("CombatValue");
                    combatValue = combatValue + itemsList[index].itemsCombatLvl[bulletsCounter];
                    PlayerPrefs.SetInt("CombatValue", combatValue);
                    mainCombatValue.text = combatValue.ToString();
                    bulletsCounter++;

                    popupNextCombatValue.text = itemsList[index].itemsCombatLvl[bulletsCounter].ToString();
                    popupCurrentLvl.text = bulletsCounter.ToString();
                    popupNextLvl.text = (bulletsCounter + 1).ToString();
                    popupItemNextValue.text = itemsList[index].itemsLvlValue[bulletsCounter].ToString();
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index, 1);
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index + GameConfiguration.KEY_ITEM_LVL_NUMBER, bulletsCounter);
                    itemsList[index].itemsLvlText.text = "Lvl " + bulletsCounter;
                    itemsList[index].itemsLockedImages.gameObject.SetActive(false);
                    itemsList[index].itemsUnlockedText.text = itemsList[index].itemsUnlockedNames;

                    
                }
                
                break;
            case 3:
                if (reloadCounter <= 3)
                {
                    popupItemCuurentValue.text = "+ " + itemsList[index].itemsLvlValue[reloadCounter] + "%";
                    popupCurrentCombatValue.text = itemsList[index].itemsCombatLvl[reloadCounter].ToString();
                    combatValue = PlayerPrefs.GetInt("CombatValue");
                    combatValue = combatValue + itemsList[index].itemsCombatLvl[reloadCounter];
                    PlayerPrefs.SetInt("CombatValue", combatValue);
                    mainCombatValue.text = combatValue.ToString();
                    reloadCounter++;
                
                    popupNextCombatValue.text = itemsList[index].itemsCombatLvl[reloadCounter].ToString();
                    popupCurrentLvl.text = reloadCounter.ToString();
                    popupNextLvl.text = (reloadCounter + 1).ToString();
                    popupItemNextValue.text = itemsList[index].itemsLvlValue[reloadCounter].ToString();
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index, 1);
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index + GameConfiguration.KEY_ITEM_LVL_NUMBER, reloadCounter);
                    itemsList[index].itemsLvlText.text = "Lvl " + reloadCounter;
                    itemsList[index].itemsLockedImages.gameObject.SetActive(false);
                    itemsList[index].itemsUnlockedText.text = itemsList[index].itemsUnlockedNames;

                    
                }
                
                break;
            case 4:
                if (fireCounter <= 3)
                {
                    popupItemCuurentValue.text = "+ " + itemsList[index].itemsLvlValue[fireCounter] + "%";
                    popupCurrentCombatValue.text = itemsList[index].itemsCombatLvl[fireCounter].ToString();
                    combatValue = PlayerPrefs.GetInt("CombatValue");
                    combatValue = combatValue + itemsList[index].itemsCombatLvl[fireCounter];
                    PlayerPrefs.SetInt("CombatValue", combatValue);
                    mainCombatValue.text = combatValue.ToString();
                    fireCounter++;
                
                    popupNextCombatValue.text = itemsList[index].itemsCombatLvl[fireCounter].ToString();
                    popupCurrentLvl.text = fireCounter.ToString();
                    popupNextLvl.text = (fireCounter + 1).ToString();
                    popupItemNextValue.text = itemsList[index].itemsLvlValue[fireCounter].ToString();
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index, 1);
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index + GameConfiguration.KEY_ITEM_LVL_NUMBER, fireCounter);
                    itemsList[index].itemsLvlText.text = "Lvl " + fireCounter;
                    itemsList[index].itemsLockedImages.gameObject.SetActive(false);
                    itemsList[index].itemsUnlockedText.text = itemsList[index].itemsUnlockedNames;

                    
                }
                
                break;
            case 5:
                if (powerCounter <= 3)
                {
                    popupItemCuurentValue.text = "+ " + itemsList[index].itemsLvlValue[powerCounter] + "%";
                    popupCurrentCombatValue.text = itemsList[index].itemsCombatLvl[powerCounter].ToString();
                    combatValue = PlayerPrefs.GetInt("CombatValue");
                    combatValue = combatValue + itemsList[index].itemsCombatLvl[powerCounter];
                    PlayerPrefs.SetInt("CombatValue", combatValue);
                    mainCombatValue.text = combatValue.ToString();
                    powerCounter++;
                
                    popupNextCombatValue.text = itemsList[index].itemsCombatLvl[powerCounter].ToString();
                    popupCurrentLvl.text = powerCounter.ToString();
                    popupNextLvl.text = (powerCounter + 1).ToString();
                    popupItemNextValue.text = itemsList[index].itemsLvlValue[powerCounter].ToString();
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index, 1);
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + index + GameConfiguration.KEY_ITEM_LVL_NUMBER, powerCounter);
                    itemsList[index].itemsLvlText.text = "Lvl " + powerCounter;
                    itemsList[index].itemsLockedImages.gameObject.SetActive(false);
                    itemsList[index].itemsUnlockedText.text = itemsList[index].itemsUnlockedNames;

                    
                }
                break;
        }
    }
    void UnlockedPopupEnable()
    {
        popupUnlock.sprite = itemsList[randomNumber].itemsUnlockedSprite;
        popupUnlock.rectTransform.sizeDelta = new Vector2(345, 345);
        popupTitle.text = itemsList[randomNumber].itemsUnlockedNames;
        popupMsg.text = itemsList[randomNumber].itemsUnlockedMsg;
        popupItem.text = itemsList[randomNumber].itemsPopupName;
        unlockedPopup.SetActive(true);
        //Invoke("UnlockedPopupDisable", 2f);
    }

    void UnlockedPopupDisable()
    {
        unlockedPopup.SetActive(false);
    }

    private void OnDisable()
    {
        
    }
}
