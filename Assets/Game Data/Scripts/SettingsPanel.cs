using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{

    public GameObject _UIRoot;
    public Button Sound;
    public Button Music;

    public Image Sound_child_Img;
    public Image Music_child_Img;

    public Text soundText;
    public Text musicText;

    public Sprite OnImage;
    public Sprite OffImage;
    public Camera[] otherCams;
    public AudioSource GPSounds;
    public Slider smoothMouse;
    
    private static SettingsPanel _instance = new SettingsPanel();
    private SettingsPanel() { }
    public static SettingsPanel Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Show()
    {

        _UIRoot.SetActive(true);
        int siblingIndex = TopBarGameplay.Instance.transform.GetSiblingIndex() - 1; //To show Cashbundle on top but behind TopBar
        transform.SetSiblingIndex(siblingIndex);

    }

    public void Hide()
    {
        _UIRoot.SetActive(false);
    }

    // Use this for initialization
    void OnEnable()
    {
        for (int i = 0; i < otherCams.Length; i++)
        {
            if (otherCams[i])
                otherCams[i].gameObject.SetActive(false);

            MainMenuController.CamsAreoff = true;
        }

        if (!GVSoundManager.Instance.isSoundON())
        {
            Sound_child_Img.sprite = OffImage;
            //soundText.text = "OFF";
        }
        else
        {
            Sound_child_Img.sprite = OnImage;
            //soundText.text = "ON";
        }

        if (!GVSoundManager.Instance.isMusicON())
        {
            Music_child_Img.sprite = OffImage;
            //musicText.text = "OFF";
        }
        else
        {
            Music_child_Img.sprite = OnImage;
            //musicText.text = "ON";
        }
        CheckSensivity();
    }

    public void CheckSensivity()
    {
        smoothMouse.value = (PlayerPrefs.GetFloat("touchsmooths", 0.6f) / 3);
    }
    private void OnDisable()
    {
        for (int i = 0; i < otherCams.Length; i++)
        {
            if (otherCams[i])
                otherCams[i].gameObject.SetActive(true);

            MainMenuController.CamsAreoff = false;
        }
    }

    public void ChangeSensitivity()
    {
        PlayerPrefs.SetFloat("touchsmooths", smoothMouse.value * 3);
    }

    public void OnSoundBtnClick()
    {

        if (!GVSoundManager.Instance.isSoundON())
        {

            GVSoundManager.Instance.setSoundValue(1);
            Sound_child_Img.sprite = OnImage;
            //soundText.text = "ON";
        }
        else
        {
            GVSoundManager.Instance.setSoundValue(0);
            Sound_child_Img.sprite = OffImage;
            //soundText.text = "OFF";
        }

        GVSoundManager.Instance.PlayBtnClickSound();
    }

    public void OnMusicBtnClick()
    {
        if (!GVSoundManager.Instance.isMusicON())
        {
            GVSoundManager.Instance.setMusicValue(1);
            Music_child_Img.sprite = OnImage;
            GVSoundManager.Instance.PlayBGMusic("Music_MainMenu");
            //musicText.text = "ON";
        }
        else
        {
            GVSoundManager.Instance.setMusicValue(0);
            Music_child_Img.sprite = OffImage;
            GVSoundManager.Instance.StopBgMusic();
            //musicText.text = "OFF";
        }
    }
}
