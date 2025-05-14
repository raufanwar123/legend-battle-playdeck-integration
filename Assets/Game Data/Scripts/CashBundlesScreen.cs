using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CashBundlesScreen : MonoBehaviour
{
	 public GameObject _UIRoot;
	 public Button _UnlockAllWeaponsBtn;
	 public Button _StarterPackBtn;
	 public Button _MediKit1Btn;
	 public Button _MediKit2Btn;
	 public Button _MediKit3Btn;
	 public Button _GrenadePack1Btn;
	 public Button _GrenadePack2Btn;
	 public Button _ClaimFreeBtn;
	 public Button _CoinPack1Btn;
	 public Button _CoinPack2Btn;
	 public Button _CoinPack3Btn;
	 public Button _RemoveAdsBtn;
	 Text WatchThreeVideoTxt;
	 public Text _TextCoinsTotal;
	 int videosCount = 3;


	 private static CashBundlesScreen _instance = new CashBundlesScreen();
	 private CashBundlesScreen() { }
	 public static CashBundlesScreen Instance
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
	 private void OnEnable()
	 {
		  UpdateCash();
	 }
	 public void UpdateCash()
	 {
		  Debug.Log("Cash : " + GameConfiguration.getTotalCash().ToString());
		  _TextCoinsTotal.text = GameConfiguration.getTotalCash().ToString();
	 }
	 // Start is called before the first frame update
	 void Start()
	 {
		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_UNLOCK_WEAPONS) == 1)
			   _UnlockAllWeaponsBtn.interactable = false;

		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_STARTER_PACK) == 1)
			   _StarterPackBtn.interactable = false;

		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_FREE_CLAIM_1000) == 1)
			   _ClaimFreeBtn.interactable = false;

		  if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_REMOVEADS) == 1)
			   _RemoveAdsBtn.interactable = false;



		  if (WatchThreeVideoTxt)
		  {
			   if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.VideoCount) != 0)
					WatchThreeVideoTxt.text = "WATCH " + GameConfiguration.GetIntegerKeyValue(GameConfiguration.VideoCount) + " VIDEOS";
			   else
					WatchThreeVideoTxt.text = "WATCH 3 VIDEOS";
		  }
	 }

	 public void ShowCashBundleScreen()
	 {
		  if (GVSoundManager.Instance)
		  {
			   GVSoundManager.Instance.PlayBtnClickSound();
		  }

		  _UIRoot.SetActive(true);
		  int siblingIndex = TopBarGameplay.Instance.transform.GetSiblingIndex() - 1; //To show Cashbundle on top but behind TopBar
		  transform.SetSiblingIndex(siblingIndex);
	 }
	 void CheckScene()
	 {
		  Scene scene = SceneManager.GetActiveScene();
		  string myScene = scene.name;
		  if (myScene.Equals("MainMenu"))
		  {

		  }
	 }
	 public void HideCashBundleScreen()
	 {
		  if (GVSoundManager.Instance)
		  {
			   GVSoundManager.Instance.PlayBtnClickSound();
		  }
		  //CheckScene();
		  _UIRoot.SetActive(false);
	 }
	 public void UnlockAllWeaponsSuccess()
	 {
		  GameConfiguration.UnlockAllGunsCallBack();
		  //GenericPopup.Instance.unlockAllWeaponsPopup.SetActive(false);
		  _UnlockAllWeaponsBtn.interactable = false;
	 }
	 public void StarterPackSuccess()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_STARTER_PACK, 1);
		  _StarterPackBtn.interactable = false;
	 }
	 public void MeditKit1Success()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_MEDIKIT1, 1);
	 }
	 public void MeditKit2Success()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_MEDIKIT2, 1);
	 }
	 public void MeditKit3Success()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_MEDIKIT3, 1);
	 }
	 public void GrenadePack1Success()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_GRENADEPACK1, 1);
	 }
	 public void GrenadePack2Success()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_GRENADEPACK2, 1);
	 }
	 public void ClaimFreeBtnClick()
	 {
		  if (GVSoundManager.Instance)
		  {
			   GVSoundManager.Instance.PlayBtnClickSound();
		  }
		  //admanager.instance.ShowRewardedVideoAd(1);
		  // if (PlayerPrefs.GetInt(GameConfiguration.KEY_FREE_CLAIM_1000, 0) == 0)
		  // {
		  //   GameConfiguration.setTotalCash(GameConfiguration.getTotalCash() + 1000);
		  // PlayerPrefs.SetInt(GameConfiguration.KEY_FREE_CLAIM_1000, 1);
		  //_ClaimFreeBtn.interactable = false;
		  //GenericPopup.Instance.SetMessageText("Success", " 1000 Free Cash.!");
		  // }
		  //   UpdateCash();
	 }
	 public void ThreeVideosBtnClick()
	 {
		  if (GVSoundManager.Instance)
		  {
			   GVSoundManager.Instance.PlayBtnClickSound();
		  }



	 }
	 public void UnlockALLSuccess()
	 {
		  GameConfiguration.SetUnlockALLinPlayerPref();
		  //GenericPopup.Instance.unlockAllGamePopup.SetActive(false);
	 }
	 public void CoinPack1Success()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_COIN1, 1);
		  UpdateCash();
	 }
	 public void CoinPack2Success()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_COIN2, 1);
		  UpdateCash();
	 }
	 public void CoinPack3Success()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_COIN3, 1);
		  UpdateCash();
	 }
	 public void RemoveAddSuccess()
	 {
		  GameConfiguration.SetIntegerKeyValue(GameConfiguration.KEY_REMOVEADS, 1);
		  _RemoveAdsBtn.interactable = false;
	 }
	 public void WatchVideoCoinsBtnClick()
	 {

	 }
	 public void UnlockAllLevelSuccess()
	 {
		  GameConfiguration.UnlockAllLevelCallBack();
		  //GenericPopup.Instance.unlockAllLevelsPopup.SetActive(false);
	 }

	 public void InAppFailedCallback()
	 {
		  GenericPopup.Instance.SetMessageText("Failed", "");
	 }
	 public void InAppBtnClick(string id)
	 {
		  if (GVSoundManager.Instance)
			   GVSoundManager.Instance.PlaySound("InappSuccess");
		  switch (id)
		  {
			   case "coins_pack1":
					GameConfiguration.InAppCashPurchases(id);
					CoinPack1Success();
					break;
			   case "coins_pack2":
					GameConfiguration.InAppCashPurchases(id);
					CoinPack2Success();
					break;
			   case "coins_pack3":
					GameConfiguration.InAppCashPurchases(id);
					CoinPack3Success();
					break;
			   case "medikit_pack1":
					MeditKit1Success();
					GameConfiguration.setMediKit(50);
					break;
			   case "medikit_pack2":
					MeditKit2Success();
					GameConfiguration.setMediKit(30);
					break;
			   case "medikit_pack3":
					MeditKit3Success();
					GameConfiguration.setMediKit(10);
					break;
			   case "grenade_pack1":
					GrenadePack1Success();
					GameConfiguration.setGrenade(50);
					break;
			   case "grenade_pack2":
					GrenadePack2Success();
					GameConfiguration.setGrenade(30);
					break;
			   case "grenade_pack3":
					GameConfiguration.setGrenade(10);
					break;
			   case "remove_ads":
					if (PlayerPrefs.GetInt(GameConfiguration.KEY_REMOVEADS, 0) == 1)
						 return;
					else
					{
						 GameConfiguration.setRemoveAds();
						 RemoveAddSuccess();
						 //AdmobAds.instance.RemoveAds(true);
						 _RemoveAdsBtn.interactable = false;
					}
					break;
			   case "starter_pack":
					StarterPackSuccess();
					GameConfiguration.StarterPackCallBack();
					break;
			   case "all_weapons":
					if (PlayerPrefs.GetInt(GameConfiguration.KEY_UNLOCK_WEAPONS, 0) == 1)
						 return;
					else
					{
						 GameConfiguration.UnlockAllGunsCallBack();
						 UnlockAllWeaponsSuccess();
						 _CoinPack1Btn.interactable = false;
						 //if (GVSoundManager.Instance)
						 //    GVSoundManager.Instance.PlaySound("InappSuccess");
					}

					break;
			   case "unlock_all":
					if (PlayerPrefs.GetInt(GameConfiguration.KEY_UNLOCK_ALL, 0) == 1)
						 return;
					else
					{
						 GameConfiguration.SetUnlockALLinPlayerPref();
					}
					break;
			   default:
					break;
		  }
	 }

	 public void ShowIcon()
	 {

	 }
}
