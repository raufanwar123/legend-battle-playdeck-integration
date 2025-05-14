using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityStandardAssets.CrossPlatformInput;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public GameObject playerControlsObj;
    public GameObject gamePlayUIObj;

    public Button pauseBtn;
    public Button changeViewBtn;
    private CameraControl cameraControl;

    public Text headshotTxt;

    public GameObject TimePanel;
    public Text timeTxt;

    public GameObject EnemyCountObj;
    public Text enemyCountTxt;

    public GameObject LevelCompletePanel;
    public GameObject LevelFailedPanel;
    public GameObject PausePanel;

    public GameObject ObjectivePanel;
    public Text ObjectivesTxt;
    public Button OkBtn;

    public GameObject LoadingPanel;

    public Image FaderImg;

    private void Awake()
    {
        instance = this;
        pauseBtn.onClick.AddListener(ShowPausePanel);
        LoadingPanel.SetActive(false);
        OkBtn.onClick.AddListener(OnOkBtnCick);
        changeViewBtn.onClick.AddListener(OnChangeViewClick);

        cameraControl = FindObjectOfType<CameraControl>();

        ShowPlayerCOntrols(false);

    }

    public void SHowEnemyCountObj(bool val)
    {
        EnemyCountObj.SetActive(val);
    }

    public void EnemyCountUpdate(int count)
    {
        enemyCountTxt.text = count.ToString();
    }

    public void SelectWeapon()
    {
        FindObjectOfType<PlayerWeapons>().SelectWeaponBySam(6);
    }
    public void OnChangeViewClick()
    {
        if (cameraControl)
        {
            if (cameraControl.thirdPersonActive)
            {
                cameraControl.thirdPersonActive = false;
            }
            else
            {
                cameraControl.thirdPersonActive = true;
            }
        }
    }

    public void ShowPlayerCOntrols(bool val)
    {
        playerControlsObj.SetActive(val);
        gamePlayUIObj.SetActive(val);
    }

    public void ShowHeadshotTxt()
    {
        if (GameController.instance)
        {
            GameController.instance.headshotCount++;
        }
        print("Headshot kill");
        headshotTxt.gameObject.SetActive(true);
        Invoke("hideHeadshotTxt", 1.2f);
    }

    void hideHeadshotTxt()
    {
        headshotTxt.gameObject.SetActive(false);
    }

    public void ShowLevelCompletePanel()
    {
        Time.timeScale = 0;
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlaySound("LevelCOmplete");
        LevelCompletePanel.SetActive(true);
        

        Debug.LogError("UICONTROLLER: LevelComplete");
    }

    public void ShowLevelFailedPanel()
    {
        Time.timeScale = 0;
        Debug.LogError("UICONTROLLER: Levelfail");
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlaySound("Failed");

        LevelFailedPanel.SetActive(true);
        
    }

    public void ShowPausePanel()
    {
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlayBtnClickSound();

        Time.timeScale = 0;
        Debug.LogError("UICONTROLLER: LevelPause");

        

        PausePanel.SetActive(true);

        
    }

    public void ShowObjectivePanel(string data)
    {
        ObjectivePanel.SetActive(true);
        ObjectivesTxt.text = data;
    }

    public void OnOkBtnCick()
    {
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlayBtnClickSound();

        if (LevelController.instance)
        {
            if (LevelController.instance.hasCutScene)
            {
                LevelController.instance.cutSceneObj.SetActive(true);
                Invoke("StartHidingScene", 5);
            }
            else if (LevelController.instance.levelType == LevelType.WaveMode)
            {
                LevelController.instance.WaveManagerObj.SetActive(true);
                ShowPlayerCOntrols(true);
            }
            else
            {
                ShowPlayerCOntrols(true);
            }
        }
        Time.timeScale = 1;
        ObjectivePanel.SetActive(false);
        //LevelController.instance.StartTime = true;
    }

    private void StartHidingScene()
    {
        StartCoroutine(FadeIn());
        Invoke("HideCutscene", 2);
    }

    private void HideCutscene()
    {
        ShowPlayerCOntrols(true);
        if (LevelController.instance)
        {
            if (LevelController.instance.hasCutScene)
            {
                LevelController.instance.cutSceneObj.SetActive(false);
            }
        }
    }

    public IEnumerator FadeIn()
    {
        if (FaderImg.gameObject.activeSelf == false)
        {
            FaderImg.gameObject.SetActive(true);
        }
        float elapsedTime = 0.0f;
        Color c = FaderImg.color;
        while (elapsedTime < 2)
        {
            yield return new WaitForSeconds(0.01f);
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / 2);
            FaderImg.color = c;

            if (FaderImg.color.a == 1)
            {
                StartCoroutine(FadeOut());
                StopCoroutine(FadeIn());
            }
        }
    }

    public IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color c = FaderImg.color;
        while (elapsedTime < 2)
        {
            yield return new WaitForSeconds(0.01f);
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / 2);
            FaderImg.color = c;
            if (FaderImg.color.a == 0)
            {
                FaderImg.gameObject.SetActive(false);
                StopAllCoroutines();
            }
        }
    }
}
