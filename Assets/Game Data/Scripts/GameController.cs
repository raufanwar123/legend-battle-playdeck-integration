using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject PlayerCharacter;

    public GameObject[] Levels;
    public int currentLevel = 0;

    public PlayerWeapons playerWeaponComponent;
    public WeaponBehavior[] weaponsUsedinGame;
    public int currentWeaponIndex = 0;
    public int enemyKilled = 0;
    public int headshotCount = 0;

    [HideInInspector]
    public int reward = 0;

    int isFirstLevelPlayedFirstTime = 0;
    private void Awake()
    {
        instance = this;
        //if (GVAdsManager.Instance)
        //{
        //    GVAdsManager.Instance.RequestInterstitial();
        //    GVAdsManager.Instance.RequestRewardedVideo();
        //}
        if (!playerWeaponComponent)
        {
            playerWeaponComponent = FindObjectOfType<PlayerWeapons>();
        }
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        currentLevel = GameConfiguration.GetIntegerKeyValue(GameConfiguration.SelectedLevelKey) - 1;
        isFirstLevelPlayedFirstTime = GameConfiguration.GetIntegerKeyValue(GameConfiguration.isPlayedFirstTimeKey);
        currentWeaponIndex = GameConfiguration.GetIntegerKeyValue(GameConfiguration.SelectedItemKey) - 1;

        if (currentLevel >= Levels.Length || currentLevel < 0)
        {
            currentLevel = 0;
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.SelectedLevelKey , currentLevel +1);
        }

        for (int i = 0; i < Levels.Length; i++)
        {
            if (i == currentLevel)
            {
                Levels[i].gameObject.SetActive(true);
                GetObjectivesDetail(Levels[i]);
            }
            else
            {
                Levels[i].gameObject.SetActive(false);
            }
        }

        LevelController.instance.Init();
        SelectWeapon();
    }

    public void SelectWeapon()
    {
        for (int i = 0; i < weaponsUsedinGame.Length; i++)
        {
            weaponsUsedinGame[i].haveWeapon = false;
        }

        weaponsUsedinGame[currentWeaponIndex].haveWeapon = true;

        int index = weaponsUsedinGame[currentWeaponIndex].weaponNumber;

        playerWeaponComponent.SelectWeaponBySam(index);
    }

    public void GetObjectivesDetail(GameObject level)
    {
        string objectiveDetails =  level.GetComponent<LevelController>().LevelObjectiveString;
        UIController.instance.ShowObjectivePanel(objectiveDetails);
    }

    int currentCompleteLevel = 0;
    public void SetLevelComplete()
    {
        LevelController.instance.isLevelCompleted = true;
        currentLevel = GameConfiguration.GetIntegerKeyValue(GameConfiguration.SelectedLevelKey);
        currentCompleteLevel = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CompletedLevelsKey);

        if (currentLevel >= currentCompleteLevel)
        {
            if (currentLevel == 0)
            {
                if (isFirstLevelPlayedFirstTime == 0)
                {
                    GameConfiguration.SetIntegerKeyValue(GameConfiguration.isPlayedFirstTimeKey , 1);
                }
            }
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.CompletedLevelsKey, currentLevel + 1);
        }

        int currCash = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
        reward = LevelController.instance.levelCompletionReward;
        reward = reward + (enemyKilled * 10) + (headshotCount + 50);
        GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, currCash + reward);

//        Invoke("ShowLevelCompletePanel" , 3);
        ShowLevelCompletePanel();
    }

    private void ShowLevelCompletePanel()
    {
        if (UIController.instance)
        {
            UIController.instance.ShowLevelCompletePanel();
        }
    }
    public void ShowLevelFailedPanel()
    {
        //LevelController.instance.StartTime = false;
        if (UIController.instance)
        {
            UIController.instance.ShowLevelFailedPanel();
        }
    }
}
