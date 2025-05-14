using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewLevelSelection : MonoBehaviour
{
    public GameObject multiPlayerLevelsParent;
    public GameObject singlePlayerLevelsParent;
    public Text unlockText;
    public Text titleText;
    public GameObject unlockedSheet;
    public int[] levelsToPlayToUnlockNewEnvironment;

    public Text[] levelsText;
    public Text[] siteNameText;
    public string[] siteName;
    public Image[] siteMapImages;
    public Sprite[] siteMapSprites;
    public GameObject[] envirnSelectedImage;
    public GameObject[] LevelButtons;
    //public GameObject[] LevelLocks;
    public GameObject[] LevelGlows;
    public Sprite[] UnlockedImage;
    public GameObject NextButton;
    public GameObject GunSelection;
    public GameObject MenuSelection;
    public GameObject RewardPanel;

    int Completed_Level;
    int currentEnvironment;
    int totalPlayedLevels;

    private void OnEnable()
    {
        SetInitailValues();
    }
    void SetInitailValues()
    {
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
        {
            singlePlayerLevelsParent.SetActive(true);
            multiPlayerLevelsParent.SetActive(false);
            NextButton.SetActive(false);
            titleText.text = "SELECT ENVIRONMENT";
            int j = 0;
            for (int i = siteMapSprites.Length - 1; i >= 0; i--)
            {
                siteMapImages[j].sprite = siteMapSprites[i];
                siteNameText[j].text = siteName[i];
                j++;
            }
        }
        else
        {
            singlePlayerLevelsParent.SetActive(false);
            multiPlayerLevelsParent.SetActive(true);
            NextButton.SetActive(true);
            titleText.text = "SELECT MODE";
            for (int i = 0; i < siteMapSprites.Length; i++)
            {
                siteNameText[i].text = siteName[i];
                siteMapImages[i].sprite = siteMapSprites[i];
            }
        }
        
        for (int i = 0; i < envirnSelectedImage.Length; i++)
        {
            envirnSelectedImage[i].SetActive(false);
        }
        for (int i = 0; i < LevelGlows.Length; i++)
        {
            LevelGlows[i].SetActive(false);
        }
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            LevelButtons[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < MainMenuController.instance.levelLocks.Length; i++)
        {
            MainMenuController.instance.levelLocks[i].SetActive(true);
        }
        MainMenuController.instance.levelLocks[0].SetActive(false);
        SetCurrentEnvironment(0);
        LevelClicked(0);
    
}
    void Start()
    {
        currentEnvironment = GameConfiguration.GetSelectedEnvironment();
        Completed_Level = GameConfiguration.GetCompleteLevel();
        Debug.Log("Completed_Level : " + Completed_Level);
        //Completed_Level = PlayerPrefs.GetInt(currentEnvironment + "Complete_Level");
        //if (PlayerPrefs.GetInt("OnceEnvironment" + currentEnvironment) == 0)
        //{
        //    PlayerPrefs.SetInt("UnlockedEnvironment" + currentEnvironment, 1);
        //    PlayerPrefs.SetInt("OnceEnvironment" + currentEnvironment, 1);
        //    currentEnvironment = PlayerPrefs.GetInt("SelectedEnvironment");
        //    PlayerPrefs.SetInt(currentEnvironment + "Complete_Level", 1);
        //    PlayerPrefs.SetInt("SelectedEnvironment", 0);
        //}
        for (int i = 0; i < levelsToPlayToUnlockNewEnvironment.Length; i++)
        {
            totalPlayedLevels += GameConfiguration.GetCompleteLevel(i);/*PlayerPrefs.GetInt(i + "Complete_Level");*/
        }
        //DisableLevels();
        for (int i = 0; i < Completed_Level; i++)
        {
            MainMenuController.instance.levelLocks[i].SetActive(false);
        }
        //if (currentEnvironment == 0)
        //{
        //    unlockedSheet.SetActive(false);
        //}


        //unlockText.text = totalPlayedLevels + " / " + levelsToPlayToUnlockNewEnvironment[0];
        
        EnvironmentSelectedImage(currentEnvironment);
        //SetCurrentEnvironment(currentEnvironment);
    }

    public void SetCurrentEnvironment(int num)
    {
        if (MainMenuController.instance)
            MainMenuController.instance.OnButtonClickSound();
        GameConfiguration.SetSelectedEnvironment(num);
        //PlayerPrefs.SetInt("SelectedEnvironment", num);
        //currentEnvironment = PlayerPrefs.GetInt("SelectedEnvironment");
        currentEnvironment = GameConfiguration.GetSelectedEnvironment();


        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 1)
        {
            if (num != 0)
            {
                if (totalPlayedLevels >= levelsToPlayToUnlockNewEnvironment[num])
                {
                    //if (PlayerPrefs.GetInt("OnceEnvironment" + currentEnvironment) == 0)
                    if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.UnlockedEnvironmentKey + num) == 0)
                    {
                        GameConfiguration.SetUnlockedEnvironment(currentEnvironment, 1);
                        GameConfiguration.SetOnceEnvironment(currentEnvironment, 1);
                        GameConfiguration.SetCompleteLevel(currentEnvironment, 1);
                        //PlayerPrefs.SetInt("UnlockedEnvironment" + currentEnvironment, 1);
                        //PlayerPrefs.SetInt("OnceEnvironment" + currentEnvironment, 1);
                        //PlayerPrefs.SetInt(currentEnvironment + "Complete_Level", 1);
                        LevelClicked(1); //junaid added this for multiplayer game 
                    }
                    DisableLevels();
                    LevelButtons[0].GetComponent<Button>().interactable = true;
                    MainMenuController.instance.levelLocks[0].SetActive(false);
                    EnvironmentSelectedImage(currentEnvironment);
                    for (int i = 0; i < LevelGlows.Length; i++)
                    {
                        LevelGlows[i].SetActive(false);
                    }
                }
                else
                {
                    DisableLevels();
                    EnvironmentSelectedImage(currentEnvironment);
                    for (int i = 0; i < LevelGlows.Length; i++)
                    {
                        LevelGlows[i].SetActive(false);
                    }
                }
            }
            if (num == 0)
            {
                DisableLevels();
                LevelButtons[0].GetComponent<Button>().interactable = true;
                MainMenuController.instance.levelLocks[0].SetActive(false);
                EnvironmentSelectedImage(currentEnvironment);
                for (int i = 0; i < LevelGlows.Length; i++)
                {
                    LevelGlows[i].SetActive(false);
                }
            }
            GameConfiguration.SetSelectedLevel(currentEnvironment, num);
            //PlayerPrefs.SetInt(currentEnvironment + "Level_Num", num);
            LevelGlows[0].SetActive(true);
        }
        else
        {
            if (num != 0)
            {
                Debug.Log(num);
                if (totalPlayedLevels >= levelsToPlayToUnlockNewEnvironment[num])
                {
                    //if (PlayerPrefs.GetInt("OnceEnvironment" + currentEnvironment) == 0)
                    if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.UnlockedEnvironmentKey + num) == 0)
                    {
                        GameConfiguration.SetUnlockedEnvironment(currentEnvironment, 1);
                        GameConfiguration.SetOnceEnvironment(currentEnvironment, 1);
                        GameConfiguration.SetCompleteLevel(currentEnvironment, 1);
                        //PlayerPrefs.SetInt("UnlockedEnvironment" + currentEnvironment, 1);
                        //PlayerPrefs.SetInt("OnceEnvironment" + currentEnvironment, 1);
                        //PlayerPrefs.SetInt(currentEnvironment + "Complete_Level", 1);
                    }
                }
            }

            if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_UNLOCK_LEVELS) == 0)
            {
                DisableLevels();
            }
            //int levels = PlayerPrefs.GetInt(currentEnvironment + "Complete_Level");
            int levels = GameConfiguration.GetIntegerKeyValue(currentEnvironment + GameConfiguration.CompleteLevelKey, 1);
            

            for (int i = 0; i < LevelButtons.Length; i++)
            {
                LevelButtons[i].GetComponent<Image>().sprite = UnlockedImage[currentEnvironment];
            }
            if (totalPlayedLevels >= levelsToPlayToUnlockNewEnvironment[num])
            {
                for (int i = 0; i < levels; i++)
                {
                    LevelButtons[i].GetComponent<Button>().interactable = true;
                    MainMenuController.instance.levelLocks[i].SetActive(false);
                }
            }
            for (int i = 0; i < LevelGlows.Length; i++)
            {
                LevelGlows[i].SetActive(false);
            }
            EnvironmentSelectedImage(currentEnvironment);
        }
    }

    void EnvironmentSelectedImage(int index)
    {
        for (int i = 0; i < envirnSelectedImage.Length; i++)
        {
            envirnSelectedImage[i].SetActive(false);
        }
        envirnSelectedImage[index].SetActive(true);
    }

    void DisableLevels()
    {
        for (int i = 0; i < LevelButtons.Length; i++)
        {
            MainMenuController.instance.levelLocks[i].SetActive(true);
            LevelButtons[i].GetComponent<Button>().interactable = false;
        }
    }
    void SetLevelsText(int num)
    {
        //for (int i = 0; i < levelsText.Length; i++)
        //{
        //    levelsText[i].text = (num + 1) + "-" + (i + 1);
        //}
    }

    //void Update()
    //{
    //    for (int i = 0; i < PlayerPrefs.GetInt(currentEnvironment + "Complete_Level"); i++)
    //    {
    //        LevelButtons[i].GetComponent<Button>().interactable = true;
    //    }
    //}

    public void LevelClicked(int Num)
    {
         for (int j = 0; j < LevelGlows.Length; j++)
            {
                LevelGlows[j].SetActive(false);
                LevelGlows[Num].SetActive(true);
            }
        GameConfiguration.SetSelectedLevel(Num);
        GameConfiguration.SetSelectedEnvironment(currentEnvironment);
        //PlayerPrefs.SetInt(currentEnvironment + "Level_Num", Num);
        NextButton.SetActive(true);
        MainMenuController.instance.OnButtonClickSound();
    }
    public void UnlockAllLevelSuccess()
    {
        GameConfiguration.UnlockAllLevelCallBack();
        for (int i = 0; i < levelsToPlayToUnlockNewEnvironment.Length; i++)
        {
            totalPlayedLevels += i + GameConfiguration.GetCompleteLevel();
        }
        SetCurrentEnvironment(currentEnvironment);
        
    }
    public void GoToGunSelection()
    {
        
        if (GenericPopup.Instance)
            //GenericPopup.Instance.ShowRandomOfferPack();
        GunSelection.SetActive(true);
        MainMenuController.instance.OnButtonClickSound();

    }

    public void GoToMenu()
    {
        MainMenuController.instance.MainMenuPanel.SetActive(true);
        
        gameObject.SetActive(false);
        MainMenuController.instance.OnButtonClickSound(true);

    }

    public void GoToModeSelection()
    {
        if (MainMenuController.instance)
        {
            MainMenuController.instance.modeSelectionPanel.SetActive(true);
            gameObject.SetActive(false);
            MainMenuController.instance.OnButtonClickSound(true);
        }
    }

    public void OnWatchVideoBtnClickLevelUnlock()
    {
        int currentCash = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
        

    }
    void AwardPanelFalse()
    {
        RewardPanel.SetActive(false);
    }

    public void FortNite()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.VforVictory.FortBattlegroundsRoyaleMiniClash");
    }
    public void Aircraft()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.VForVictory.StrikerBallsAvoidObstacles");
    }
    public void MergeCar()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.vforvictory.JellyManJumpingSuperFunGame");
    }
    public void JackRunner()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.VForVictory.JackRushRunner");
    }
}
