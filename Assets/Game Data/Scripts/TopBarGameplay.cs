using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopBarGameplay : MonoBehaviour
{

    public Text _TextCoinsTOtal;
    public string _MoreGamesLink = "https://play.google.com/store/apps/dev?id=6675166732386446811";
    public string _sceneName = "MainMenu";
    public GameObject _UIRoot;
    public GameObject _ExitPanel;
    public GameObject _ExitBtn;
    

    private static TopBarGameplay _instance = new TopBarGameplay();
    private TopBarGameplay() { }

    public static TopBarGameplay Instance
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


    

    private void OnEnable()
    {
        _TextCoinsTOtal.text = GameConfiguration.getTotalCash().ToString();
    }

    public void ShowTopBar()
    {
        _UIRoot.SetActive(true);
    }

    public void HideTopBar()
    {
        _UIRoot.SetActive(false);
    }

    public void ShowExitBtn()
    {
        //_ExitBtn.SetActive(true);
        
    }

    public void HideExitBtn()
    {
        _ExitBtn.SetActive(false);
    }
    public void OpenSettingPanel()
    {
        
        SettingsPanel.Instance.Show();
    }

    public void RewardedVideoCoins()
    {

        
}
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(_sceneName);
    }
    public void OpenMoreGames()
    {
        Application.OpenURL(_MoreGamesLink);
    }

    public void CashPlusBtnClick()
    {
        
        CashBundlesScreen.Instance.ShowCashBundleScreen();
    }

    public void ExitBtnClick()
    {
        
            

            _ExitPanel.SetActive(true);
            _UIRoot.SetActive(false);
        
    }

    public void OnExitYes()
    {
        Application.Quit();
    }

    public void OnExitNO()
    {
        _ExitPanel.SetActive(false);
        _UIRoot.SetActive(true);
    }

    public void RefreshTotalCoinsTxt()
    {
        _TextCoinsTOtal.text = GameConfiguration.getTotalCash().ToString();
    }
}
