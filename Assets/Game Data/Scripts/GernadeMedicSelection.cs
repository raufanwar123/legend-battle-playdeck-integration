using UnityEngine;
using UnityEngine.UI;

public class GernadeMedicSelection : MonoBehaviour
{
    public GameObject gernadeModel, medicKitModel;
    public GameObject gernadeSpecs, medicKitSpecs;
    public GameObject rightArrow, leftArrow;

    public int gernadePrice, medicKitPrice;

    public string medicDescriptionString, gernadeDescriptionString;

    public Text itemPriceText, descriptionText, totalGernadeText, totalMedicKitsText, _TextCoinsTotal;

    private int totalGernade, totalMedic;
    private int currentItemIndex;
    private void OnEnable()
    {
        UpdateCash();
    }
    void UpdateCash()
    {
        _TextCoinsTotal.text = GameConfiguration.getTotalCash().ToString();
    }
    private void Start()
    {
        totalGernade = PlayerPrefs.GetInt("TotalGernades");
        totalMedic = PlayerPrefs.GetInt("TotalMedicKits");
        Initialization();
    }

    void Update()
    {
        totalGernadeText.text = GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_GRENADE).ToString();
        totalMedicKitsText.text = GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MEDIKIT).ToString();
    }

    void Initialization()
    {
        medicKitModel.SetActive(false);
        gernadeModel.SetActive(true);

        gernadeSpecs.SetActive(true);
        medicKitSpecs.SetActive(false);

        itemPriceText.text = gernadePrice.ToString();
        descriptionText.text = gernadeDescriptionString;

        totalGernadeText.text = totalGernade.ToString();
        totalMedicKitsText.text = totalMedic.ToString();

        totalGernadeText.transform.parent.gameObject.SetActive(true);
        totalMedicKitsText.transform.parent.gameObject.SetActive(false);

        leftArrow.SetActive(false);
        rightArrow.SetActive(true);

        currentItemIndex = 0;
    }

    public void RightArrow()
    {
        medicKitModel.SetActive(true);
        gernadeModel.SetActive(false);

        gernadeSpecs.SetActive(false);
        medicKitSpecs.SetActive(true);

        itemPriceText.text = medicKitPrice.ToString();
        descriptionText.text = medicDescriptionString;

        totalGernadeText.text = totalGernade.ToString();
        totalMedicKitsText.text = totalMedic.ToString();

        totalGernadeText.transform.parent.gameObject.SetActive(false);
        totalMedicKitsText.transform.parent.gameObject.SetActive(true);

        rightArrow.SetActive(false);
        leftArrow.SetActive(true);

        currentItemIndex = 1;
    }

    public void LeftArrow()
    {
        Initialization();
    }

    public void BuyItem()
    {
        int cashVal = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
        if (currentItemIndex == 0)
        {
            if (gernadePrice <= cashVal)
            {
                PlayerPrefs.SetInt("TotalGernades", (PlayerPrefs.GetInt("TotalGernades") + 1));
                GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, cashVal - gernadePrice);
                totalGernade += 1;
                PlayerPrefs.SetInt("TotalGernades", totalGernade);
                totalGernadeText.text = totalGernade.ToString();
            }
            else if (gernadePrice > cashVal) 
            {
                //if (MainMenuController.instance)
                //    MainMenuController.instance.OpenStore();
                //cashBundlesManager.showCashBundle();// Comment By Waseem
            }
            UpdateCash();
        }
        else if (currentItemIndex == 1)
        {
            if (medicKitPrice <= cashVal)
            {
                PlayerPrefs.SetInt("TotalMedicKits", (PlayerPrefs.GetInt("TotalMedicKits") + 1));
                GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, cashVal - medicKitPrice);
                totalMedic += 1;
                PlayerPrefs.SetInt("TotalMedicKits", totalMedic);
                totalMedicKitsText.text = totalMedic.ToString();
            }
            else if (medicKitPrice > cashVal)
            {
                //if (MainMenuController.instance)
                //    MainMenuController.instance.OpenStore();
                //cashBundlesManager.showCashBundle();// Comment By Waseem
            }
            UpdateCash();
        }
    }

    public void Back()
    {
        if (EnemyCounter.instance != null)
            EnemyCounter.instance.isMedicKit = true;

        if (MainMenuController.instance)
        {
            MainMenuController.instance.ItemSelectionPanel.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
        }
        Time.timeScale = 1;
        if (GameStat.instance)
        {
            GameStat.instance.playerCanvas.SetActive(true);
            GameStat.instance.miniMapCanvas.SetActive(true);
            GameStat.instance.playerCrossHairImage.enabled = true;
            gameObject.SetActive(false);
        }
        //if (LevelSettings.instance)
        //{
        //    if (PlayerPrefs.GetString("InfiniteMode") == "No")
        //    {
        //        int SelectedLevel = PlayerPrefs.GetInt("Level_Num");
        //        LevelSettings.instance.Levels[SelectedLevel].SetActive(true);
        //    }
        //}

    }

    public void Next()
    {
        MainMenuController.instance.gunMedicSelectionPanel.SetActive(false);
        MainMenuController.instance.ShowLoadingScreen();
        //     MainMenuController.instance.counterScreen.SetActive(true);
        TopBarGameplay.Instance.HideTopBar();
        TopBarGameplay.Instance.HideExitBtn();
        //  this.gameObject.SetActive(false);
        //        MainMenuController.instance.gunMedicSelectionPanel.SetActive(false);

       // GoogleAllAds.Instance.ShowInterstitialAd();
    }
}
