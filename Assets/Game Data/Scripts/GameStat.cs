using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameStat : MonoBehaviour
{
	 public static GameStat instance;

	 public GameObject headShotImage;
	 public GameObject zoomImage;
	 public GameObject scorePanel;
	 public GameObject killsPanel;
	 public GameObject timerIcon;
	 public GameObject onPickupShow;


	 public GameObject miniMapCanvas, playerCanvas;
	 public Image playerCrossHairImage;
	 public GameObject[] fireBtns;
	 public GameObject PausePanel, FailPanel, /*FailPanelInifiniteMode,*/ CompletePanel, StatsPanel, weaponSelectionPanel, victoryDancePanel/*miniMapCanas*/, BannerAdBackground;
	 [Header("Enemy Health Bar Data")]
	 public Sprite[] flagSprites;
	 public Sprite[] iconSprites;
	 public string[] enemyNames;
	 public string[] teamNames;
	 public GameObject killPanel;
	 public GameObject killBarPrefab;
	 public Transform contentPanel;
	 //public KillBarUI killBarUI;

	 [Header("Talents Bar Data")]
	 public GameObject talentsBar;
	 public Text health;
	 public Text moreCoins;
	 public Text moreBullets;
	 public Text rapidReload;
	 public Text rapidFire;
	 public Text morePower;
	 public GameObject healthObject;
	 public GameObject moreCoinsObject;
	 public GameObject moreBulletsObject;
	 public GameObject rapidReloadObject;
	 public GameObject rapidFireObject;
	 public GameObject morePowerObject;
	 public Image flagImage;
	 public float flagFillImageSpeed;

	 private bool RotateFlag;
	 int SelectedLevel;
	 public AudioSource UIMusic;
	 public GameObject GamePlayUI;
	 public Text KillReward, CollectedText;
	 public int[] RewardOfLevels;
	 public Text RewardText;
	 public static int DummyVar;
	 public GameObject RateUsPanel;
	 public static int myPoints = 0, oponentsPoints = 0; //junaid added this and below to count the kills 
	 public Text myKills, oponentKills;
	 // Use this for initialization

	 //junaid adding the following lines for the victry panel
	 public GameObject VictoryPanel;
	 public Text VictoryDefeatText, playerScore, oponentScore;
	 public FPSPlayer fpsPlayer;
	 //public WeaponBehavior weaponBehavior;
	 public PlayerWeapons playerWeapons;
	 public GameObject moreCoinsImage;
	 public Text moreCoinsText;
	 public GameObject LoadingPanel;
	 public GameObject loadingScreen;
	 public GameObject rebornScreen;
	 public Text objectiveText;
	 public string[] objectiveMsg;
	 public GameObject weaponGiftPanel;
	 public GameObject[] giftBoxes;
	 [HideInInspector]
	 public bool isGameComplete;
	 [HideInInspector]
	 public bool isGameOver;
	 void SingleTon()
	 {
		  if (instance != null && instance != this)
		  {
			   Destroy(gameObject);
		  }
		  else
		  {
			   instance = this;
		  }
	 }

	 void Awake()
	 {
		  SingleTon();
		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
		  {
			   LoadingPanel.SetActive(false);
			   objectiveText.text = objectiveMsg[0];
			   fpsPlayer.hitPoints = 200f;
			   fpsPlayer.maximumHitPoints = 200f;
			   killsPanel.SetActive(true);
			   timerIcon.SetActive(true);
		  }
		  else
		  {
			   LoadingPanel.SetActive(true);
			   objectiveText.text = objectiveMsg[1];
			   fpsPlayer.hitPoints = 20f;
			   fpsPlayer.maximumHitPoints = 20f;
			   scorePanel.SetActive(true);
		  }

		  GiftBoxesState(true);
	 }
	 public void CheckAutoShoot()
	 {
		  CheckTalents();
	 }
	 [HideInInspector]
	 public string killer;
	 [HideInInspector]
	 public string killed;
	 GameObject a;
	 public void KillBarPanel()
	 {
		  killPanel.SetActive(true);
		  a = Instantiate(killBarPrefab, transform) as GameObject;
		  a.transform.SetParent(contentPanel);
		  a.transform.localScale = Vector2.one;
		  a.GetComponent<KillBarUI>().DisplayUI(killer, killed);
	 }

	 void Start()
	 {
		  isGameComplete = false;
		  isGameOver = false;
		  Time.timeScale = 1;
		  RotateFlag = true;
		  SelectedLevel = PlayerPrefs.GetInt("Level_Num");
		  DummyVar = 0;
		  RewardText.text = RewardOfLevels[SelectedLevel].ToString();
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.StopBgMusic();
	 }
	 public void MultiplayerVictory(string statement)  //junaid add the function
	 {
		  Time.timeScale = 0;
		  VictoryPanel.SetActive(true);
		  VictoryDefeatText.text = "" + statement;
		  playerScore.text = "" + myPoints;
		  oponentScore.text = "" + oponentsPoints;

	 }

	 public void NextButtonClicked()
	 {
		  VictoryPanel.SetActive(false);
		  Time.timeScale = 1;

		  if (myPoints > oponentsPoints)
			   CompleteFun(); // Invoke("CompleteFun", 10);
		  else
			   FailFunCall(); // Invoke("FailFunCall", 10);
							  //admanager.instance.ShowInterstitialAd();

		//myPoints = 0;
		//oponentsPoints = 0;

		//GoogleAllAds.Instance.ShowInterstitialAd();
	}






	 void FailFunCall()
	 {
		  FailFun("Greater Number of Enemies Kill");
	 }
	 public void CheckTalents()
	 {
		  if (PlayerPrefs.GetInt("CombatValue") > 0)
		  {
			   talentsBar.SetActive(true);
			   Debug.Log("Check for Talents");
			   if (fpsPlayer)
			   {
					Debug.Log("Player Not Null");
					if (fpsPlayer.healthLvl > 0)
					{
						 if (fpsPlayer.healthLvl == 1)
						 {
							  health.text = "15%";
						 }
						 else if (fpsPlayer.healthLvl == 2)
						 {
							  health.text = "30%";
						 }
						 else if (fpsPlayer.healthLvl == 3)
						 {
							  health.text = "45%";
						 }
						 else if (fpsPlayer.healthLvl == 4)
						 {
							  health.text = "60%";
						 }
						 healthObject.SetActive(true);
					}
			   }
			   if (/*weaponBehavior*/playerWeapons.CurrentWeaponBehaviorComponent)
			   {
					Debug.Log("weaponBehavior Not Null");
					if (playerWeapons.CurrentWeaponBehaviorComponent.ammoLvl > 0)
					{
						 if (playerWeapons.CurrentWeaponBehaviorComponent.ammoLvl == 1)
						 {
							  moreBullets.text = "15%";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.ammoLvl == 2)
						 {
							  moreBullets.text = "30%";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.ammoLvl == 3)
						 {
							  moreBullets.text = "45%";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.ammoLvl == 4)
						 {
							  moreBullets.text = "60%";
						 }
						 moreBulletsObject.SetActive(true);
					}
					if (playerWeapons.CurrentWeaponBehaviorComponent.fireRateLvl > 0)
					{
						 if (playerWeapons.CurrentWeaponBehaviorComponent.fireRateLvl == 1)
						 {
							  rapidFire.text = "15%";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.fireRateLvl == 2)
						 {
							  rapidFire.text = "35%";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.fireRateLvl == 3)
						 {
							  rapidFire.text = "50";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.fireRateLvl == 4)
						 {
							  rapidFire.text = "60";
						 }
						 rapidFireObject.SetActive(true);
					}
					if (playerWeapons.CurrentWeaponBehaviorComponent.reloadLvl > 0)
					{
						 if (playerWeapons.CurrentWeaponBehaviorComponent.reloadLvl == 1)
						 {
							  rapidReload.text = "10%";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.reloadLvl == 2)
						 {
							  rapidReload.text = "20%";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.reloadLvl == 3)
						 {
							  rapidReload.text = "30%";
						 }
						 else if (playerWeapons.CurrentWeaponBehaviorComponent.reloadLvl == 4)
						 {
							  rapidReload.text = "40%";
						 }
						 rapidReloadObject.SetActive(true);
					}
			   }
			   if (coinsLvl > 0)
			   {
					if (coinsLvl == 1)
					{
						 moreCoins.text = "15%";
					}
					else if (coinsLvl == 2)
					{
						 moreCoins.text = "30%";
					}
					else if (coinsLvl == 3)
					{
						 moreCoins.text = "45%";
					}
					else if (coinsLvl == 4)
					{
						 moreCoins.text = "60%";
					}
					moreCoinsObject.SetActive(true);
			   }
			   Invoke("DisableTalentsBar", 2f);
		  }
	 }
	 void DisableTalentsBar()
	 {
		  talentsBar.SetActive(false);
	 }
	 bool showNativeRectAds = true;

	 public void CompleteFun()
	 {
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.StopBgMusic();
		  if (GVSoundManager.Instance && GVSoundManager.Instance.isSoundON())
		  {
			   GVSoundManager.Instance.PlaySound("iSR-Well_Done");
			   GVSoundManager.Instance.PlaySound("LevelCOmplete");
		  }

		  MyNPCWaveManager.instance.useTime = false;


		  KillReward.text = ((gameObject.GetComponent<EnemyCounter>().LevelEnemies[SelectedLevel])).ToString();
		  int Score = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, Score + RewardOfLevels[SelectedLevel]);

		  int current_Environment_Number = GameConfiguration.GetSelectedEnvironment();
		  int current_Level_Number = GameConfiguration.GetSelectedLevel(current_Environment_Number);
		  int completeLevels = GameConfiguration.GetCompleteLevel(current_Environment_Number);


		  current_Level_Number += 1;
		  if (current_Level_Number == completeLevels)
		  {
			   GameConfiguration.SetCompleteLevel(GameConfiguration.GetCompleteLevel(current_Environment_Number) + 1);

		  }
		  GameConfiguration.SetSelectedLevel(current_Environment_Number, current_Level_Number);


		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 1)
		  {


			   if (current_Environment_Number < 3)
			   {
					current_Environment_Number += 1;
					GameConfiguration.SetSelectedEnvironment(current_Environment_Number);
			   }
			   else
			   {
					GameConfiguration.SetSelectedEnvironment(0);

			   }

		  }
		  else
		  {

		  }

		  if ((current_Level_Number + 1) % 2 == 0)
		  {
			   if (PlayerPrefs.GetInt("rated", 0) == 0)
			   {
					RateUsPanel.SetActive(true);
					showNativeRectAds = false;
			   }
		  }
		  //admanager.instance.ShowInterstitialAd();
		  //admanager.instance.hideCentreUpBanner();

		  Invoke("Complete1", 0f); //junaid comment it because we want to show the complete panel without any delay
								   //Complete1();waseem comment it because we want to show the complete panel with delay
								   //GamePlayUI.SetActive (false);
		  RotateFlag = false;
	 }

	 IEnumerator EnableStatsPanelCR()
	 {
		  yield return new WaitForSecondsRealtime(1);
		  StatsPanel.SetActive(true);

		  //BannerAdBackground.SetActive(false);

	 }
	 public void FireBtnDisable(bool state)
	 {
		  for (int i = 0; i < fireBtns.Length; i++)
		  {
			   fireBtns[i].SetActive(state);
		  }
	 }
	 public void Complete1()
	 {
		  CompletePanel.SetActive(true);
		  //BannerAdBackground.SetActive(false);



		  MyNPCWaveManager.instance.GameFailrComplete();
		  StartCoroutine(EnableStatsPanelCR());
		  if (showNativeRectAds)
		  {

		  }


		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + 1) == 1)
		  {
			   Invoke("MoreCoins", 10f);
		  }
		  //Time.timeScale = 0;
	 }
	 int coinsLvl;
	 int percentageCoins;
	 void MoreCoins()
	 {
		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + 1) == 1)
		  {
			   coinsLvl = GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_ITEM_UNLOCKED + 1 + GameConfiguration.KEY_ITEM_LVL_NUMBER);
			   Debug.Log("Ammo Lvl : " + coinsLvl);
			   if (coinsLvl == 1)
			   {
					percentageCoins = 15;
			   }
			   else if (coinsLvl == 2)
			   {
					percentageCoins = 30;
			   }
			   else if (coinsLvl == 3)
			   {
					percentageCoins = 45;
			   }
			   else if (coinsLvl == 4)
			   {
					percentageCoins = 60;
			   }
			   moreCoinsImage.SetActive(true);
			   moreCoinsText.text = "+" + percentageCoins + "%";
			   if (MyNPCWaveManager.instance)
			   {
					MyNPCWaveManager.instance.AddMoreCoins(percentageCoins);
			   }
		  }

	 }
	 string FailDueTo = "";
	 public void FailFun(string Failtype)
	 {

		  FailDueTo = Failtype;
		  MyNPCWaveManager.instance.GameFailrComplete();

		  Fail1();
		  RotateFlag = false;
	 }

	 public void Fail1()
	 {
		  if (GVSoundManager.Instance)
		  {
			   GVSoundManager.Instance.StopBgMusic();
		  }
		  if (GVSoundManager.Instance && GVSoundManager.Instance.isSoundON())
		  {
			   GVSoundManager.Instance.PlaySound("Failed");
		  }
		  if (PlayerPrefs.GetString("InfiniteMode") == "No")
		  {
			   FailPanel.SetActive(true);
			   //BannerAdBackground.SetActive(false);
			   StartCoroutine(EnableStatsPanelCR());


			   int current_Environment_Number = GameConfiguration.GetSelectedEnvironment();
			   int current_Level_Number = GameConfiguration.GetSelectedLevel();
			   if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
			   {

			   }
			   else
			   {

			   }
		  }
		  else if (PlayerPrefs.GetString("InfiniteMode") == "Yes")
		  {


		  }
		  //admanager.instance.ShowInterstitialAd();
		  //admanager.instance.hideCentreUpBanner();

		  Time.timeScale = 0;
	 }
	 public void GiftBoxesState(bool status)
	 {
		  for (int i = 0; i < giftBoxes.Length; i++)
		  {
			   giftBoxes[i].SetActive(status);
		  }
	 }

	 public void PauseFun()
	 {
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.PlayBtnClickSound();
		  PausePanel.SetActive(true);
		  Time.timeScale = 0;

		//GoogleAllAds.Instance.ShowInterstitialAd();
	}

	 void SetTimeScaleToZero()
	 {
		  if (PausePanel.activeInHierarchy)
			   Time.timeScale = 0;
	 }

	 public void ResumeFun()
	 {
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.PlayBackBtnClickSound();

		  PausePanel.SetActive(false);
		  //BannerAdBackground.SetActive(true);
		  //admanager.instance.showbannercentreUp();

		  Time.timeScale = 1;
	 }

	 public void Next()
	 {
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.PlayBtnClickSound();

		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 1)
			   LoadingPanel.SetActive(true);
		  else
			   loadingScreen.SetActive(true);
		//GoogleAllAds.Instance.ShowInterstitialAd();


		DummyVar = 1;


		  myPoints = 0;
		  oponentsPoints = 0;


		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 1)
		  {
			   int currentScene = SceneManager.GetActiveScene().buildIndex;
			   switch (currentScene)
			   {
					case 3:
						 currentScene = 4;
						 // SceneManager.LoadScene(3);
						 break;
					case 4:
						 currentScene = 5;
						 // SceneManager.LoadScene(4);
						 break;
					case 5:
						 currentScene = 6;
						 //   SceneManager.LoadScene(5);
						 break;
					case 6:
						 currentScene = 3;
						 //  SceneManager.LoadScene(2);
						 break;
			   }
			   StartCoroutine(loadingScreen.GetComponent<Loading>().LoadScene(currentScene));
		  }
		  else
		  {
			   StartCoroutine(loadingScreen.GetComponent<Loading>().LoadScene(SceneManager.GetActiveScene().buildIndex));

			   //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		  }
	 }

	 public void Restart()
	 {
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.PlayBtnClickSound();

		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 1)
			   LoadingPanel.SetActive(true);
		  else
			   loadingScreen.SetActive(true);


		  myPoints = 0;
		  oponentsPoints = 0;
		  StartCoroutine(loadingScreen.GetComponent<Loading>().LoadScene(SceneManager.GetActiveScene().buildIndex));
		//   SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

		//GoogleAllAds.Instance.ShowInterstitialAd();
	}

	 public void Home()
	 {
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.PlayBtnClickSound();

		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 1)
			   LoadingPanel.SetActive(true);
		  else
			   loadingScreen.SetActive(true);

		  //admanager.instance.ShowInterstitialAd();

		  myPoints = 0;
		  oponentsPoints = 0;
		  StartCoroutine(loadingScreen.GetComponent<Loading>().LoadScene(2));
		//SceneManager.LoadScene(1);

		//GoogleAllAds.Instance.ShowInterstitialAd();

	}
	 public void DeactiveObject()
	 {
		  StartCoroutine(DisableHeadShot());
	 }
	 public IEnumerator DisableHeadShot()
	 {
		  yield return new WaitForSecondsRealtime(2f);
		  headShotImage.SetActive(false);
	 }
	 private bool isClick = false;

	 public void WeaponSelectionPanel()
	 {
		  if (!isClick)
		  {
			   weaponSelectionPanel.SetActive(true);
			   isClick = true;
		  }
		  else
		  {
			   weaponSelectionPanel.SetActive(false);
			   isClick = false;
		  }
	 }
}
