using System.Collections;
//using ControlFreak2;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayPanel : MonoBehaviour
{


    [Tooltip("Required for almost all panels")]
    public Button HomeBtn;
    [Tooltip("Required for almost all panels")]
    public Button ReplayBtn;
    [Tooltip("Required for Pause, failed and complete panels ONLY")]
    public Button resumeBtn;
    [Tooltip("Required for Complete Panel ONLY")]
    public Button nextBtn;
    [Tooltip("Required for Complete Panel ONLY")]
    public Text RewardsTxt;

    [Tooltip("Required for failed and complete panels ONLY")]
    public Text enemykillsTxt;
    [Tooltip("Required for failed and complete panels ONLY")]
    public Text headshotCountTxt;

    [Header("Pause")]
    public Image SoundBtn;
    public Text SoundTxt;
    public Image MusicBtn;
    public Text MusicTxt;
    public Image TargetBtn;
    public Text TargetTxt;
    public Slider SmoothMouseSlider;
    public SmoothMouseLook TouchPadFar;
    public Sprite On;
    public Sprite Off;

    public void Awake()
    {
        //if (SmoothMouseSlider)
        //    SmoothMouseSlider.value = (PlayerPrefs.GetFloat("touchsmooths", 0.4f)/3);

        //if (TouchPadFar)
        //{
        //    TouchPadFar.SetTouchSmoothing(SmoothMouseSlider.value);
        //}

    }
    // Use this for initialization
    void Start()
    {
        HomeBtn.onClick.AddListener(OnHomeBtnClick);
        ReplayBtn.onClick.AddListener(OnReplayBtnCLick);
        if (resumeBtn)
        {
            resumeBtn.onClick.AddListener(OnResumeBtnClick);
        }
        if (nextBtn)
        {
            nextBtn.onClick.AddListener(OnNextBtnClick);
        }
        #region Comment
        //if (LevelController.instance)
        //{
        //    if (LevelController.instance.isLevelCompleted)
        //    {
        //        print("this is reward  : " + LevelController.instance.levelCompletionReward);
        //        RewardsTxt.text = GameController.instance.reward.ToString();
        //        SHowStats();

        //    }
        //    else if (enemykillsTxt && headshotCountTxt)
        //    {
        //        SHowStats();
        //    }

        //    else
        //    {
        //        if (enemykillsTxt)
        //        {
        //            enemykillsTxt.gameObject.transform.parent.gameObject.SetActive(false);
        //        }
        //        if (headshotCountTxt)
        //        {
        //            headshotCountTxt.gameObject.transform.parent.gameObject.SetActive(false);
        //        }
        //        if (RewardsTxt)
        //        {
        //            RewardsTxt.gameObject.transform.parent.gameObject.SetActive(false);
        //        }
        //    }

        //}
        #endregion

        StartCoroutine(Reward_Animation(MyNPCWaveManager.instance.activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[MyNPCWaveManager.instance.current_Level_Number], enemykillsTxt, 1, 0.1f));
        StartCoroutine(Reward_Animation(GameStat.instance.RewardOfLevels[MyNPCWaveManager.instance.current_Level_Number], RewardsTxt, 5, 0.05f, 1.8f));
    }
    IEnumerator Reward_Animation(int amount, Text text, int amountAddInTemp, float time = 0.01f, float delayTime = 0.8f)
    {
        //int temp = 0;
        //text.text = "";
        yield return new WaitForSecondsRealtime(0f);
        //while (temp != amount)
        //{
        //    temp += amountAddInTemp;
        //    Debug.Log("Total Loop");
        //    text.text = temp.ToString();
        //    GameStat.instance.typerSound.PlayOneShot(GameStat.instance.typerClip);
        //    yield return new WaitForSecondsRealtime(time);
        //}
    }

    public void SHowStats()
    {
        //enemykillsTxt.text = GameController.instance.enemyKilled.ToString();
        //headshotCountTxt.text = GameController.instance.headshotCount.ToString();
    }

    private void OnEnable()
    {
        //if (SmoothMouseSlider)
        //    SmoothMouseSlider.value = (PlayerPrefs.GetFloat("touchsmooths", 0.4f) / 3);

        //if (TouchPadFar)
        //{
        //    TouchPadFar.SetTouchSmoothing(SmoothMouseSlider.value);
        //}
        //if (TopBarGameplay.Instance)
        //{
        //    TopBarGameplay.Instance.ShowTopBar();
        //}


        if (GVSoundManager.Instance)
        {
            if (SoundBtn)
            {
                SoundBtn.sprite = GVSoundManager.Instance.isSoundON() ? On : Off;
            }
            if (MusicBtn)
            {
                MusicBtn.sprite = GVSoundManager.Instance.isMusicON() ? On : Off;
            }
        }
        if (SmoothMouseSlider)
        {
            SmoothMouseSlider.value = PlayerPrefs.GetFloat("touchsmooths", 0.6f) / 3;
        }
        if (TargetBtn)
        {
            TargetBtn.sprite = PlayerPrefs.GetInt(GameConfiguration.IsTragetAssistOn, 1) == 1 ? On : Off;
        }
    }

    private void OnDisable()
    {
        //if (SmoothMouseSlider)
        //    SmoothMouseSlider.value = (PlayerPrefs.GetFloat("touchsmooths", 0.4f) / 3);

        //if (TouchPadFar)
        //{
        //    TouchPadFar.SetTouchSmoothing(SmoothMouseSlider.value);
        //}
        if (TopBarGameplay.Instance)
        {
            TopBarGameplay.Instance.HideTopBar();
        }
    }
    public void ChangetouchSmoothValue()
    {
        if (TouchPadFar)
        {
            TouchPadFar.SetTouchSmoothing(SmoothMouseSlider.value * 3);
            PlayerPrefs.SetFloat("touchsmooths", (SmoothMouseSlider.value * 3));
        }
    }
    public void OnHomeBtnClick()
    {
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlayBtnClickSound();
        

        Time.timeScale = 1;
        UIController.instance.LoadingPanel.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }
    public void OnReplayBtnCLick()
    {
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlayBtnClickSound();

        

        Time.timeScale = 1;
        UIController.instance.LoadingPanel.SetActive(true);
        SceneManager.LoadScene("GamePlay");
    }

    public void OnResumeBtnClick()
    {
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlayBtnClickSound();

        
        Time.timeScale = 1;
        //if (TouchPadCf)
        //{
        //    TouchPadCf.SetTouchSmoothing(SmoothMouseSlider.value);
        //    PlayerPrefs.SetFloat("touchsmooths", SmoothMouseSlider.value);
        //}
        gameObject.SetActive(false);
    }

    public void OnNextBtnClick()
    {
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlayBtnClickSound();
        

        Time.timeScale = 1;
        int currSelectedLevel = GameConfiguration.GetIntegerKeyValue(GameConfiguration.SelectedLevelKey);
        GameConfiguration.SetIntegerKeyValue(GameConfiguration.SelectedLevelKey, currSelectedLevel + 1);
        UIController.instance.LoadingPanel.SetActive(true);
        SceneManager.LoadScene("GamePlay");
    }

    public void OnSoundBtnClick()
    {
        if (GVSoundManager.Instance)
        {
            if (GVSoundManager.Instance.isSoundON())
            {
                GVSoundManager.Instance.setSoundValue(0);
                SoundBtn.sprite = Off;
                SoundTxt.text = "OFF";
            }
            else
            {
                GVSoundManager.Instance.setSoundValue(1);
                SoundBtn.sprite = On;
                SoundTxt.text = "ON";
            }
        }
    }

    public void OnMusicBtnClick()
    {
        if (GVSoundManager.Instance)
        {
            if (GVSoundManager.Instance.isMusicON())
            {
                GVSoundManager.Instance.setMusicValue(0);
                MusicBtn.sprite = Off;
                MusicTxt.text = "OFF";
            }
            else
            {
                GVSoundManager.Instance.setMusicValue(1);
                MusicBtn.sprite = On;
                MusicTxt.text = "On";
            }
        }
    }

    public void OnTargetBtnClick()
    {
        if (PlayerPrefs.GetInt(GameConfiguration.IsTragetAssistOn, 1) == 1)
        {
            TargetBtn.sprite = Off;
            PlayerPrefs.SetInt(GameConfiguration.IsTragetAssistOn, 0);
            TargetTxt.text = "Off";
        }
        else
        {
            TargetBtn.sprite = On;
            PlayerPrefs.SetInt(GameConfiguration.IsTragetAssistOn, 1);
            TargetTxt.text = "On";
        }
    }

    public void OnSensitivityChange()
    {
        PlayerPrefs.SetFloat("touchsmooths", SmoothMouseSlider.value * 3);
    }

    public void BallSort()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.vforvictory.ColorsMatchBallPuzzlesPro");
    }
    public void Airplane()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.vforvictory.MasterPlaneFlyingSimulator");
    }
}
