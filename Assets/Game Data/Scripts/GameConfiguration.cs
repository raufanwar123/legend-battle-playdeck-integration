using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfiguration : MonoBehaviour {

    public static string CashKey = "Cash";
    public static string SelectedItemKey = "SelectedItem";
    public static string XPKey = "XP";
    //public static string SelectedLevelKey = "SelectedLevel";
    public static string CompletedLevelsKey = "CompletedLevels";
    public static string SelectedModeKey = "SelectedMode";
    public static string SelectedEnvironmentKey = "SelectedEnvironment";
    public static string SelectedLevelKey = "Level_Num";
    public static string CompleteLevelKey = "Complete_Level";
    public static string UnlockedEnvironmentKey = "UnlockedEnvironment";
    public static string OnceEnvironmentKey = "OnceEnvironment";
    public static string SoundKey = "SoundKey";
    public static string MusicKey = "MusicKey";
    public static string isPlayedFirstTimeKey = "isPlayedFirstTimeKey";
    public static string SelectedWeaponKey = "SelectedWeaponKey";
    public static string IsMainMenu = "IsMainMenu";
    public static string IsItemSelection = "IsItemSelection";
    public static string VideoCount = "VideoCount";
    public static string EnvironmentPlayed = "EnvironmentPlayed";
    public static string IsTragetAssistOn = "IsTargetAssistOn";

    // New Varibales add
    public static string KEY_UNLOCK_ALL = "UNLOCK_ALL";
    public static string KEY_FREE_CLAIM_1000 = "FREE_CLAIM_1000";
    public static string KEY_REMOVEADS = "GVNoAds_inapp";
    public static string KEY_STARTER_PACK = "STARTER_PACK";
    public static string KEY_UNLOCK_WEAPONS = "UNLOCK_WEAPONS";
    public static string KEY_UNLOCK_LEVELS = "UNLOCK_LEVELS";
    public static string KEY_GRENADE = "TotalGernades";
    public static string KEY_MEDIKIT = "TotalMedicKits";
    public static string KEY_COIN1 = "COIN1PACK1";
    public static string KEY_COIN2 = "COIN1PACK2";
    public static string KEY_COIN3 = "COIN1PACK3";
    public static string KEY_MEDIKIT1 = "MEDIKITPACK1";
    public static string KEY_MEDIKIT2 = "MEDIKITPACK2";
    public static string KEY_MEDIKIT3 = "MEDIKITPACK3";
    public static string KEY_GRENADEPACK1 = "GRENADEPACK1";
    public static string KEY_GRENADEPACK2 = "GRENADEPACK2";
    public static string KEY_MODE = "SelectMode";
    public static string KEY_ITEM_UNLOCKED = "ItemUnlocked_";
    public static string KEY_ITEM_LVL_NUMBER = "_LvlNumber_";
    public static string KEY_TEAM_MEMBER = "TeamMember";

    public static string autoFirePref = "AutoFire";
    public static string targetAssistPref = "TargetAssist";
    public static string unlockedAllWeaponsPref="UnlockedAllWeapons";
    private static int totalGernade = 0;
    private static int totalMedic = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void SetIntegerKeyValue(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
    }
    public static int GetIntegerKeyValue(string key, int val =0)
    {
        return PlayerPrefs.GetInt(key, val);
    }
    public static void SetAutoFire(int val)
    {
        SetIntegerKeyValue(autoFirePref, val);
    }
    public static int GetAutoFirePref()
    {
        return GetIntegerKeyValue(autoFirePref, 1);
    }
    public static void SetStringKeyValue(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
    }
    public static string GetStringKeyValue(string key)
    {
        return PlayerPrefs.GetString(key, " ");
    }

    public static void SetFloatKeyValue(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
    }
    public static float GetFloatKeyValue(string key)
    {
        return PlayerPrefs.GetFloat(key, 0);
    }
    public static void SetSelectedEnvironment(int index)
    {
        SetIntegerKeyValue(SelectedEnvironmentKey, index);
    }

    public static int GetSelectedEnvironment()
    {
        return GetIntegerKeyValue(SelectedEnvironmentKey);
    }
    public static void SetSelectedLevel(int lvl)
    {
        SetIntegerKeyValue(GetSelectedEnvironment() + SelectedLevelKey, lvl);
    }

    public static int GetSelectedLevel()
    {
        return GetIntegerKeyValue(GetSelectedEnvironment() + SelectedLevelKey); ;
    }

    public static void SetSelectedLevel(int Env, int lvl)
    {
        SetIntegerKeyValue(Env + SelectedLevelKey, lvl);
    }

    public static int GetSelectedLevel(int Env)
    {
        return GetIntegerKeyValue(Env + SelectedLevelKey); ;
    }

    public static void SetTargetAssist(int val)
    {
        PlayerPrefs.SetInt(targetAssistPref, val);
    }


    public static int GetTargetAssist()
    {
        return PlayerPrefs.GetInt(targetAssistPref, 0);
    }

    public static void SetCompleteLevel(int lvl)
    {
        SetIntegerKeyValue(GetSelectedEnvironment() + CompleteLevelKey, lvl);
    }

    public static int GetCompleteLevel()
    {
        return GetIntegerKeyValue(GetSelectedEnvironment() + CompleteLevelKey);
    }
    public static int GetUnlockedEnvironment()
    {
        return GetIntegerKeyValue(UnlockedEnvironmentKey);
    }
    public static void SetUnlockedEnvironment(int Env, int value)
    {
        SetIntegerKeyValue(UnlockedEnvironmentKey + Env, value);
    }
    public static int GetOnceEnvironment()
    {
        return GetIntegerKeyValue(OnceEnvironmentKey);
    }
    public static void SetOnceEnvironment(int Env, int value)
    {
        SetIntegerKeyValue(OnceEnvironmentKey + Env, value);
    }
    public static void SetCompleteLevel(int Env, int lvl)
    {
        SetIntegerKeyValue(Env + CompleteLevelKey, lvl);
    }

    public static int GetCompleteLevel(int Env)
    {
        return GetIntegerKeyValue(Env + CompleteLevelKey);
    }
    public static int GetFirstTimePlay()
    {
        return PlayerPrefs.GetInt(isPlayedFirstTimeKey);
    }
    public static void SetFirstTimePlay()
    {
        if (GetFirstTimePlay() == 0)
        {
            PlayerPrefs.SetInt(isPlayedFirstTimeKey, 1);
            PlayerPrefs.SetInt(0 + CompleteLevelKey, 1);
            SetUnlockedEnvironment(0, 1);
            SetOnceEnvironment(0, 1);
        }
    }
    // New Functions Add
    public static bool isGrenadeMediBuy = false;
    public static void setTotalCash(int value)
    {
        PlayerPrefs.SetInt(CashKey, value);
    }
    public static int getTotalCash()
    {
        return PlayerPrefs.GetInt(CashKey, 0);
    }
    public static void setRemoveAds()
    {
        PlayerPrefs.SetInt(KEY_REMOVEADS, 1);
        GenericPopup.Instance.SetMessageText("Success", " Remove ads");
        
    }

    public static void SetUnlockALLinPlayerPref()
    {
        PlayerPrefs.SetInt(KEY_UNLOCK_ALL, 1);
        PlayerPrefs.SetInt(KEY_REMOVEADS, 1);
        
        
        UnlockAllLevelCallBack();
        UnlockAllGunsCallBack();
        GenericPopup.Instance.SetMessageText("Success", " Unlock All Game");
        Debug.Log("Unlock All  Purchased");
        
    }

    public static void UnlockAllGunsCallBack()
    {
        for (int i = 0; i < GenericPopup.Instance.weaponsData.weaponsList.Length; i++)
        {
            SetIntegerKeyValue(GenericPopup.Instance.weaponsData.weaponsList[i].weaponName, 1);
        }
        SetIntegerKeyValue(KEY_UNLOCK_WEAPONS, 1);
        GenericPopup.Instance.SetMessageText("Success", " Unlock All Weapons");
        if(MainMenuController.instance)
            MainMenuController.instance.unlockedAllWeaponsBtn.gameObject.SetActive(false);
        Debug.Log("Unlock All Guns Purchased");
        
        if (ItemSelectionManager.instance)
        {
            ItemSelectionManager.instance.UnlockAllWeapons.SetActive(false);
            ItemSelectionManager.instance.NewGunSelectON(0);
            ItemSelectionManager.instance.SetGunNumbersAccordingToRFPS(1);
        }
    }



    public static void PurchasePremiumPack()
    {
        string a = "";
        for (int i = 0; i < GenericPopup.Instance.weaponsData.weaponsList.Length; i++)
        {
            a = GenericPopup.Instance.weaponsData.weaponsList[i].displayName;
            if (a=="FAMAS"|| a=="AK47" || a=="KRISS" || a=="SCAR" || a=="MP7"||a=="M249"||a=="FNMAG"||a=="AWP")
            {
                SetIntegerKeyValue(GenericPopup.Instance.weaponsData.weaponsList[i].weaponName, 1);

            }

        }
        setTotalCash(getTotalCash() + 700);
        TopBarGameplay.Instance?.RefreshTotalCoinsTxt();
        setGrenade(7);
        setMediKit(7);
        GenericPopup.Instance.SetMessageText("Success", " Unlocked Premium Pack");
        
        if (ItemSelectionManager.instance)
        {
           //ItemSelectionManager.instance.UnlockAllWeapons.SetActive(false);
            ItemSelectionManager.instance.NewGunSelectON(0);
            ItemSelectionManager.instance.SetGunNumbersAccordingToRFPS(1);
        }
    }


    public static void PurchaseProStarterPack()
    {
        string a = "";
        for (int i = 0; i < GenericPopup.Instance.weaponsData.weaponsList.Length; i++)
        {
            a = GenericPopup.Instance.weaponsData.weaponsList[i].displayName;
            if (a == "SCAR" || a == "FAMAS" || a == "AWP")
            {
                SetIntegerKeyValue(GenericPopup.Instance.weaponsData.weaponsList[i].weaponName, 1);

            }

        }
        setTotalCash(getTotalCash() + 700);
        TopBarGameplay.Instance?.RefreshTotalCoinsTxt();
        setGrenade(7);
        setMediKit(7);
        GenericPopup.Instance.SetMessageText("Success", " Unlocked ProStarter Pack");
        
        if (ItemSelectionManager.instance)
        {
            //ItemSelectionManager.instance.UnlockAllWeapons.SetActive(false);
            ItemSelectionManager.instance.NewGunSelectON(0);
            ItemSelectionManager.instance.SetGunNumbersAccordingToRFPS(1);
        }
    }



    public static void PurchaseStarterPack()
    {
    
        setTotalCash(getTotalCash() + 260000);
        TopBarGameplay.Instance?.RefreshTotalCoinsTxt();
        setGrenade(7);
        setMediKit(7);
        GenericPopup.Instance.SetMessageText("Success", "260K Coins");
        
     
    }

    public static void PurchaseExtraOrdinaryPack()
    {
        string a = "";
        for (int i = 0; i < GenericPopup.Instance.weaponsData.weaponsList.Length; i++)
        {
            a = GenericPopup.Instance.weaponsData.weaponsList[i].displayName;
            if (a == "MP7" || a == "M249" || a == "FNMAG" || a == "AWP")
            {
                SetIntegerKeyValue(GenericPopup.Instance.weaponsData.weaponsList[i].weaponName, 1);

            }

        }
        setTotalCash(getTotalCash() + 700);
        TopBarGameplay.Instance?.RefreshTotalCoinsTxt();
        setGrenade(7);
        setMediKit(7);
        GenericPopup.Instance.SetMessageText("Success", " Unlocked Extraordinary Pack");
        
        if (ItemSelectionManager.instance)
        {
            //ItemSelectionManager.instance.UnlockAllWeapons.SetActive(false);
            ItemSelectionManager.instance.NewGunSelectON(0);
            ItemSelectionManager.instance.SetGunNumbersAccordingToRFPS(1);
        }
    }



    public static void setGrenade(int value)
    {
        totalGernade = PlayerPrefs.GetInt(KEY_GRENADE, 0);
        totalGernade = totalGernade + value;
        PlayerPrefs.SetInt(KEY_GRENADE, totalGernade);
        GenericPopup.Instance.SetMessageText("Success", " Grenade X" + value);
        isGrenadeMediBuy = true;
        
    }
    public static void setMediKit(int value)
    {
        totalMedic = PlayerPrefs.GetInt(KEY_MEDIKIT, 0);
        totalMedic = totalMedic + value;
        PlayerPrefs.SetInt(KEY_MEDIKIT, totalMedic);
        GenericPopup.Instance.SetMessageText("Success", "Medikit X" + value);
        isGrenadeMediBuy = true;
        
    }
    public static void StarterPackCallBack()
    {
        SetIntegerKeyValue("SNIPER", 1);
        setGrenade(5);
        setMediKit(5);
        GenericPopup.Instance.SetMessageText("Success", " Starter Pack");
        
    }
    public static void UnlockAllLevelCallBack()
    {
        for (int j = 0; j < 8; j++)
        {
            PlayerPrefs.SetInt(j + "Complete_Level", 20);
            PlayerPrefs.SetInt("UnlockedEnvironment" + j, 1);
            PlayerPrefs.SetInt("OnceEnvironment" + j, 1);
        }
        SetIntegerKeyValue(KEY_UNLOCK_LEVELS, 1);
        GenericPopup.Instance.SetMessageText("Success", " unlcoked All Levels");
        MainMenuController.instance.unlockedAllLevelsBtn.gameObject.SetActive(false);
        
    }
    public static void InAppCashPurchases(string id)
    {
        switch (id)
        {
            case "coins_pack3":
                setTotalCash(getTotalCash() + 30000);
                TopBarGameplay.Instance.RefreshTotalCoinsTxt();
                GenericPopup.Instance.SetMessageText("Success", " BUMPER OFFER 30000 Coins");
                
                break;
            case "coins_pack2":
                setTotalCash(getTotalCash() + 120000);
                TopBarGameplay.Instance.RefreshTotalCoinsTxt();
                GenericPopup.Instance.SetMessageText("Success", " BAG OF COINS 120,000");
                
                break;
            case "coins_pack1":
                setTotalCash(getTotalCash() + 260000);
                TopBarGameplay.Instance.RefreshTotalCoinsTxt();
                GenericPopup.Instance.SetMessageText("Success", " ULTRA COINS PACKAGE 260000");
                
                break;
        }
    }
    
}
