using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GenericPopup : MonoBehaviour
{
    public Text messageText;
    public GameObject genericPopup;
    //public GameObject unlockAllWeaponsPopup;
    //public GameObject unlockAllLevelsPopup;
    //public GameObject unlockAllGamePopup;


    //public GameObject[] christmasOfferPacks;


    string sceneName;
    public WeaponsData weaponsData;
    private static GenericPopup _instance = new GenericPopup();
    private GenericPopup() { }
    private void Start()
    {
          _instance = this;
    }
    public static GenericPopup Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //public void ShowRandomOfferPack()
    //{
    //    int a = Random.Range(0, christmasOfferPacks.Length);
    //    christmasOfferPacks[a].SetActive(true);
    //}



    public void SetMessageText(string type, string msg)
    {
        //genericPopup.SetActive(true);
        if (type.Equals("Success"))
        {
            messageText.text = "You Have Been Awarded With \n" + msg;
            if (GVSoundManager.Instance)
                GVSoundManager.Instance.PlaySound("InappSuccess");
        }
        else
        {
            messageText.text = "Failed \n" + msg;
            messageText.color = Color.red;
        }
        CheckCurrentScene();
        Invoke("DelayForPopupDisable", 3f);
    }
    void CheckCurrentScene()
    {
        Scene currenScene = SceneManager.GetActiveScene();
        sceneName = currenScene.name;
        if (sceneName.Equals("GamePlay"))
        {
            Time.timeScale = 1;
        }
    }
    void DelayForPopupDisable()
    {
        Debug.Log("Pop up Disable");
        //genericPopup.SetActive(false);
    }
    public void OfferPopupActive(string type)
    {
        if (GVSoundManager.Instance)
        {
            GVSoundManager.Instance.PlayBtnClickSound();
        }
        //if (type.Equals("Unlock_All_Game") && PlayerPrefs.GetInt(GameConfiguration.KEY_UNLOCK_ALL, 0) == 0)
        //{
        //    unlockAllGamePopup.SetActive(true);
        //}
        //else if (type.Equals("Unlock_All_Weapons") && PlayerPrefs.GetInt(GameConfiguration.KEY_UNLOCK_WEAPONS, 0) == 0)
        //{
        //    unlockAllWeaponsPopup.SetActive(true);
        //}
        //else if (type.Equals("Unlock_All_Levels") && PlayerPrefs.GetInt(GameConfiguration.KEY_UNLOCK_LEVELS, 0) == 0)
        //{
        //    unlockAllLevelsPopup.SetActive(true);
        //}
    }

    public void OfferBuyBtnClick(string type)
    {
        if (GVSoundManager.Instance)
        {
            GVSoundManager.Instance.PlayBtnClickSound();
        }
        if (type.Equals("Unlock_All_Game"))
        {
            CashBundlesScreen.Instance.UnlockALLSuccess();
        }
        else if (type.Equals("Unlock_All_Weapons"))
        {
            CashBundlesScreen.Instance.UnlockAllWeaponsSuccess();
        }
        else if (type.Equals("Unlock_All_Levels"))
        {
            CashBundlesScreen.Instance.UnlockAllLevelSuccess();
        }
        else if(type.Equals("premium_pack"))
        {
            GameConfiguration.PurchasePremiumPack();
        }
        else if (type.Equals("extraordinary_pack"))
        {
            GameConfiguration.PurchaseExtraOrdinaryPack();
        }
        else if (type.Equals("prostarter_pack"))
        {
            GameConfiguration.PurchaseProStarterPack();
        }
        else if (type.Equals("starter_pack"))
        {
            GameConfiguration.PurchaseStarterPack();
        }
    }

    public void PlayBtnSound()
    {
        if (GVSoundManager.Instance)
        {
            GVSoundManager.Instance.PlayBackBtnClickSound();
        }
    }
}
