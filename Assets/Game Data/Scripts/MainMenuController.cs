using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{

	 public static MainMenuController instance;
	 public PausePanell pause;
	 public Camera[] otherCams;
	 public GameObject[] lockImages;
	 public GameObject[] levelLocks;
	 // these are mainmenu attributes
	 public GameObject dailyRewardPanel;
	 public GameObject unlockedAllWeaponsBtn;
	 public Button unlockedAllLevelsBtn;
	 public GameObject MainMenuPanel;

	 public Button StartBtn;
	 public Button settingsBtn;
	 public Button privacyBtn;
	 public Button rateUsBtn;
	 public Button moreGamesBtn;

	 public GameObject ExitPanel;
	 public Button exitYesBtn;
	 public Button exitNoBtn;

	 //these are privacy Panel attributes
	 public GameObject PrivacyPanel;
	 public string privacyLink;
	 public Button privacyYesBtn;
	 public Button privacyNoBtn;

	 //these are rateus Panel attributes
	 public GameObject RateUsPanel;
	 public Button rateYesBtn;
	 public Button rateNoBtn;

	 // these are more games Panel attributes
	 public GameObject MoreGamesPanel;
	 public string moreGamesLink;
	 public Button moreGameYesBtn;
	 public Button moreGameNoBtn;

	 public GameObject LevelSelectionPanel;
	 public GameObject ItemSelectionPanel;
	 public GameObject gunMedicSelectionPanel;
	 public GameObject modeSelectionPanel;
	 public GameObject teamSelectionPanel;
	 public GameObject talentsPanel;


	 public GameObject LoadingScreenObj;
	 //this is button click sound Object
	 public GameObject buttonClickSoundObj;
	 public GameObject LockDailyReward;
	 public Button DailyRewardBtn;
	 public string GamePlaySceneName = "GamePlay1"; // "GamePlay"
	 public GameObject RewardPanel;
	 public bool DelPrefs;
	 public static bool CamsAreoff;

	 public int currentEnvironment, levels = 0;
	 public bool testDailyreward;

	 public GameObject counterScreen; //junaid added this to add counter screenti
	 public Text cashTxt;
	 // interstitial showing images;
	 public List<Sprite> firstEnvImages;
	 public List<Sprite> secondEnvImages;
	 public List<Sprite> thirdEnvImages;

	 int imageNumber1;
	 int imageNumber2;
	 int imageNumber3;
	 private void Start()
	 {

		  //if (GenericPopup.Instance)
			 //  GenericPopup.Instance.ShowRandomOfferPack();

		  if (SettingsPanel.Instance)
		  {
			   SettingsPanel.Instance.CheckSensivity();
		  }

		  if (PlayerPrefs.GetInt("FirstTimeLoaded", 0) == 0)   //Adeel added this;
		  {
			   dailyRewardPanel.SetActive(true);
			   PlayerPrefs.SetInt("FirstTimeLoaded", 1);
		  }
		  if (instance == null)
		  {
			   instance = this;
		  }
	 }
	 public void OpenPrivacyPolicy()
	 {
		  Application.OpenURL("https://sites.google.com/view/vforvictorygamesprivacypolicy/home");
	 }
	 public void ChangeImage(int buttonNumber)
	 {
		  Image currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
		  switch (buttonNumber)
		  {
			   case 1:
					imageNumber1++;
					currentButton.sprite = firstEnvImages[imageNumber1];
					if (imageNumber1 == 2)
					{
						 imageNumber1 = -1;
					}
					break;
			   case 2:
					imageNumber2++;
					currentButton.sprite = secondEnvImages[imageNumber2];
					if (imageNumber2 == 2)
					{
						 imageNumber2 = -1;
					}
					break;
			   case 3:
					imageNumber3++;
					currentButton.sprite = thirdEnvImages[imageNumber3];
					if (imageNumber3 == 2)
					{
						 imageNumber3 = -1;
					}
					break;
			   default:
					break;
		  }
	 }
	 public void Update()
	 {

		  currentEnvironment = 0;
		  if (!testDailyreward)
			   levels = PlayerPrefs.GetInt(currentEnvironment + "Complete_Level");
		  if (levels > 3)
		  {
			   //LockDailyReward.SetActive(false);
		  }
		  else
		  {
			   //LockDailyReward.SetActive(true);
		  }

		  if (Input.GetKeyDown(KeyCode.Escape))
		  {
			   if (MainMenuPanel.activeInHierarchy && !LevelSelectionPanel.activeInHierarchy && !ItemSelectionPanel.activeInHierarchy)
			   {
					OnExitBtnClick();
			   }

		  }
		  if (ExitPanel.activeInHierarchy)
		  {
			   //CamsAreoff = !true;
		  }

		  else if (CamsAreoff)
		  {
			   for (int i = 0; i < otherCams.Length; i++)
			   {
					//if (otherCams[i])
					//otherCams[i].gameObject.SetActive(false);

			   }
		  }
		  else
		  {
			   for (int i = 0; i < otherCams.Length; i++)
			   {
					if (otherCams[i])
						 otherCams[i].gameObject.SetActive(true);

			   }
		  }

		  if (DelPrefs)
		  {
			   PlayerPrefs.DeleteAll();
		  }
		  if (Application.platform != RuntimePlatform.Android)
		  {
			   ControlFreak2.CFCursor.lockState = CursorLockMode.None;
		  }
	 }
	 private void Awake()
	 {

		  //GameConfiguration.setTotalCash(100000000);
		  Cursor.lockState = CursorLockMode.None;
		  Cursor.visible = true;


		  Time.timeScale = 1;
		  instance = this;

		  TopBarGameplay.Instance.ShowTopBar();
		  TopBarGameplay.Instance.ShowExitBtn();
		  StartBtn.onClick.AddListener(OnStartBtnClick);
		  settingsBtn.onClick.AddListener(OnSettingsBtnClick);
		  privacyNoBtn.onClick.AddListener(() => OnAllNoButtonClick(privacyNoBtn.gameObject));
		  rateNoBtn.onClick.AddListener(() => OnAllNoButtonClick(rateNoBtn.gameObject));
		  moreGamesBtn.onClick.AddListener(OnMoreGamesBtnClick);
		  moreGameNoBtn.onClick.AddListener(() => OnAllNoButtonClick(moreGameNoBtn.gameObject));
		  exitNoBtn.onClick.AddListener(() => OnAllNoButtonClick(exitNoBtn.gameObject));
		  privacyBtn.onClick.AddListener(OnPrivacyBtnClick);
		  privacyYesBtn.onClick.AddListener(() => OnPrivacyYesClick(privacyYesBtn.gameObject));
		  rateUsBtn.onClick.AddListener(OnRateUsBtnClick);
		  rateYesBtn.onClick.AddListener(() => OnRateYesClick(rateYesBtn.gameObject));
		  moreGameYesBtn.onClick.AddListener(() => OnMoreGamesYesClick(moreGameYesBtn.gameObject));

		  UpdateCash();
		  if (GameStat.DummyVar == 1)
		  {
			   LevelSelectionPanel.SetActive(true);
			   ItemSelectionPanel.SetActive(false);
		  }
		  else
		  {
			   ItemSelectionPanel.SetActive(false);
			   MainMenuPanel.SetActive(true);
		  }

		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.PlayBGMusic("Music_MainMenu");
		  if (SettingsPanel.Instance)
		  {
			   SettingsPanel.Instance.smoothMouse.value = (PlayerPrefs.GetFloat("touchsmooths", 0.6f) / 3);
		  }
	 }

	 public void OpenDailyRewardDialogue()
	 {


		  dailyRewardPanel.SetActive(true);
	 }

	 public void OnWatchVideoBtnClick()
	 {
		  OnButtonClickSound();

        int currentCash = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
        if (PlayerPrefs.GetInt("RewardKey") == 0)
        {
            GameConfiguration.setTotalCash(GameConfiguration.getTotalCash() + 1000);
            UpdateCash();

        }

    }

	public void RewardAd()
    {
		//GoogleAllAds.Instance.ShowRewardedAd(OnWatchVideoBtnClick);
	}

	 void AwardPanelFalse()
	 {
		  RewardPanel.SetActive(false);
	 }
	 public void OnStartBtnClick()
	 {
		  OnButtonClickSound();

		  //ShowLevelSelection(true);
		  modeSelectionPanel.SetActive(true);


	 }

	 public void OnExitBtnClick()
	 {
		  OnButtonClickSound(true);
		  for (int i = 0; i < otherCams.Length; i++)
		  {
			   if (otherCams[i])
					otherCams[i].gameObject.SetActive(false);

		  }

		  ExitPanel.SetActive(true);


	 }
	 public void ModeSelection(int index)
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_MODE, index);
		  ShowLevelSelection(true);
		  modeSelectionPanel.SetActive(false);

		//GoogleAllAds.Instance.ShowInterstitialAd();
	 }
	 public void GoToMenu()
	 {
		  MainMenuPanel.SetActive(true);

		  modeSelectionPanel.SetActive(false);
		  OnButtonClickSound(true);
	 }
	 public void OnExitYes()
	 {
		  OnButtonClickSound(true);

		  Application.Quit();
	 }

	 public void UpdateCash()
	 {
		  cashTxt.text = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey).ToString();
	 }

	 public void OnSettingsBtnClick()
	 {
		  if (GVSoundManager.Instance)
		  {
			   GVSoundManager.Instance.PlayBtnClickSound();
		  }

		  if (SettingsPanel.Instance)
		  {
			   SettingsPanel.Instance.Show();
		  }


	 }

	 public void onRemoveAdsBtnClick()
	 {
		  OnButtonClickSound();
		  print("Remove Ads Click!");
	 }

	 public void OnPrivacyBtnClick()
	 {
		  OnButtonClickSound();

		  PrivacyPanel.SetActive(true);
	 }

	 public void OnPrivacyYesClick(GameObject btn)
	 {
		  OnButtonClickSound();
		  Application.OpenURL(privacyLink);
		  btn.transform.parent.transform.parent.gameObject.SetActive(false);

	 }

	 public void OnRateUsBtnClick()
	 {
		  OnButtonClickSound();
		  Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);


	 }

	 public void OnRateYesClick(GameObject btn)
	 {
		  OnButtonClickSound();
		  Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
		  btn.transform.parent.transform.parent.gameObject.SetActive(false);




	 }

	 public void OnMoreGamesBtnClick()
	 {
		  OnButtonClickSound();
		  MoreGamesPanel.SetActive(true);

	 }
	 public void OnTeamSelectionBtnClick()
	 {
		  OnButtonClickSound();
		  teamSelectionPanel.SetActive(true);

		//GoogleAllAds.Instance.ShowInterstitialAd();
	 }
	 public void OnTalentsBtnClick()
	 {
		  OnButtonClickSound();
		  talentsPanel.SetActive(true);

		//GoogleAllAds.Instance.ShowInterstitialAd();
	}
	 public void OnMoreGamesYesClick(GameObject btn)
	 {
		  OnButtonClickSound();
		  Application.OpenURL(moreGamesLink);
		  btn.transform.parent.transform.parent.gameObject.SetActive(false);

	 }

	 public void ShowLoadingScreen()
	 {
		  print("loading ");

		  LoadingScreenObj.SetActive(true);
		  Invoke("LoadScene", 2f);
	 }

	 public void ShowItemSelectionPanel(bool val)
	 {
		  if (ItemSelectionManager.instance)
		  {
			   ItemSelectionManager.instance.itemViewCamera.SetActive(val);
		  }
		  ItemSelectionPanel.SetActive(val);


	 }
	 int sceneNoToLoad;

	 private void LoadScene()
	 {
		  int environmentNumber = GameConfiguration.GetSelectedEnvironment();
		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
		  {
			   switch (environmentNumber)
			   {
					case 0:
						 sceneNoToLoad = 3;
						 // SceneManager.LoadScene(2);
						 break;
					case 1:
						 sceneNoToLoad = 4;
						 // SceneManager.LoadScene(4);
						 break;
					case 2:
						 sceneNoToLoad = 5;
						 //SceneManager.LoadScene(3);
						 break;
					case 3:
						 sceneNoToLoad = 6;
						 // SceneManager.LoadScene(5);
						 break;
					case 4:
						 sceneNoToLoad = 7;
						 // SceneManager.LoadScene(5);
						 break;
					case 5:
						 sceneNoToLoad = 8;
						 // SceneManager.LoadScene(5);
						 break;
					case 6:
						 sceneNoToLoad = 9;
						 // SceneManager.LoadScene(5);
						 break;
			   }
		  }
		  else
		  {
			   switch (environmentNumber)
			   {
					case 0:
						 sceneNoToLoad = 3;
						 //   SceneManager.LoadScene(2);
						 break;
					case 1:
						 sceneNoToLoad = 4;
						 // SceneManager.LoadScene(3);
						 break;
					case 2:
						 sceneNoToLoad = 5;

						 // SceneManager.LoadScene(4);
						 break;
					case 3:
						 sceneNoToLoad = 6;

						 // SceneManager.LoadScene(5);
						 break;
					case 4:
						 sceneNoToLoad = 7;
						 // SceneManager.LoadScene(5);
						 break;
					case 5:
						 sceneNoToLoad = 8;
						 // SceneManager.LoadScene(5);
						 break;
					case 6:
						 sceneNoToLoad = 9;
						 // SceneManager.LoadScene(5);
						 break;
			   }
		  }
		  if (LoadingScreenObj)
			   StartCoroutine(LoadingScreenObj.GetComponent<Loading>().LoadScene(sceneNoToLoad));

	 }

	 public void OnAllNoButtonClick(GameObject obj)
	 {
		  OnButtonClickSound(true);
		  obj.transform.parent.transform.parent.gameObject.SetActive(false);

	 }

	 public void ShowLevelSelection(bool val)
	 {
		  LevelSelectionPanel.SetActive(val);
	 }

	 public void OnButtonClickSound(bool isBackFromAnyPanel = false)
	 {
		  if (isBackFromAnyPanel)
		  {
			   if (GVSoundManager.Instance)
					GVSoundManager.Instance.PlayBackBtnClickSound();
		  }
		  else
		  {

			   if (GVSoundManager.Instance)
					GVSoundManager.Instance.PlayBtnClickSound();
		  }



	 }

	 public void CheckForMusic()
	 {
		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.MusicKey) == 0)
		  {
			   Camera.main.GetComponent<AudioSource>().Stop();
		  }
		  else
		  {
			   Camera.main.GetComponent<AudioSource>().Play();
		  }
	 }

	 public void HideIcon()
	 {

	 }
	 public void OpenStore()
	 {
		  if (TopBarGameplay.Instance)
			   TopBarGameplay.Instance.CashPlusBtnClick();
		  if (CashBundlesScreen.Instance)
			   CashBundlesScreen.Instance.UpdateCash();



	 }

	 public void UnlockAllGameSuccess()
	 {
		  if (CashBundlesScreen.Instance)
		  {
			   CashBundlesScreen.Instance.UnlockALLSuccess();
		  }
	 }
	 public void SpecialOffer()
	 {
		  OnButtonClickSound();
		  //if (GenericPopup.Instance)
			 //  GenericPopup.Instance.ShowRandomOfferPack();
	 }
}
