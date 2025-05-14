using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanell : MonoBehaviour
{
	 [Tooltip("Required for almost all panels")]
	 public Button HomeBtn;
	 [Tooltip("Required for almost all panels")]
	 public Button ReplayBtn;
	 [Tooltip("Required for Pause, failed and complete panels ONLY")]
	 public Button resumeBtn;

	 [Header("Pause")]
	 public Image SoundBtn;
	 public Text SoundTxt;
	 public Image MusicBtn;
	 public Text MusicTxt;
	 public Image TargetBtn;
	 public Text TargetTxt;
	 public Slider SmoothMouseSlider;
	 public Sprite On;
	 public Sprite Off;
	 public SmoothMouseLook smoothMouseLook;
	 public void Awake()
	 {

	 }
	 // Use this for initialization
	 void Start()
	 {
		  if (HomeBtn)
			   HomeBtn.onClick.AddListener(OnHomeBtnClick);
		  if (ReplayBtn)
			   ReplayBtn.onClick.AddListener(OnReplayBtnCLick);
		  if (resumeBtn)
			   resumeBtn.onClick.AddListener(OnResumeBtnClick);
	 }
	 private void OnEnable()
	 {
		  if (GVSoundManager.Instance)
		  {
			   if (!GVSoundManager.Instance.isSoundON())
			   {
					SoundBtn.sprite = Off;
					SoundTxt.text = "OFF";
			   }
			   else
			   {
					SoundBtn.sprite = On;
					SoundTxt.text = "ON";
			   }

			   if (!GVSoundManager.Instance.isMusicON())
			   {
					MusicBtn.sprite = Off;
					MusicTxt.text = "OFF";
			   }
			   else
			   {
					MusicBtn.sprite = On;
					MusicTxt.text = "ON";
			   }
		  }
		  CheckSensivity();
		  if (TargetBtn)
		  {
			   if (!PlayerPrefs.HasKey(GameConfiguration.IsTragetAssistOn))
			   {
					PlayerPrefs.SetInt(GameConfiguration.IsTragetAssistOn, 1);
			   }
			   TargetBtn.sprite = PlayerPrefs.GetInt(GameConfiguration.IsTragetAssistOn, 1) == 1 ? On : Off;
			   TargetTxt.text = PlayerPrefs.GetInt(GameConfiguration.IsTragetAssistOn, 1) == 1 ? "ON" : "OFF";
		  }
	 }
	 public void CheckSensivity()
	 {
		  if (SmoothMouseSlider)
		  {
			   SmoothMouseSlider.value = (PlayerPrefs.GetFloat("touchsmooths", 0.6f) / 3);
		  }
	 }
	 public void OnHomeBtnClick()
	 {
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.PlayBackBtnClickSound();

		  Time.timeScale = 1;
		  if (UIController.instance.LoadingPanel != null)
		  {
			   UIController.instance.LoadingPanel.SetActive(true);
		  }
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
	 Scene m_Scene;
	 string sceneName;
	 public void OnMusicBtnClick()
	 {
		  m_Scene = SceneManager.GetActiveScene();
		  sceneName = m_Scene.name;

		  if (GVSoundManager.Instance)
		  {
			   if (!GVSoundManager.Instance.isMusicON())
			   {
					GVSoundManager.Instance.setMusicValue(1);
					MusicBtn.sprite = On;
					MusicTxt.text = "ON";
					if (sceneName.Equals("MainMenu"))
					{
						 GVSoundManager.Instance.PlayBGMusic("Music_MainMenu");
					}
			   }
			   else
			   {
					GVSoundManager.Instance.setMusicValue(0);
					MusicBtn.sprite = Off;
					MusicTxt.text = "OFF";
					GVSoundManager.Instance.StopBgMusic();
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
		  smoothMouseLook.SetTouchSmoothing(SmoothMouseSlider.value * 3);
	 }
}

