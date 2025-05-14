using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class admanager : MonoBehaviour
{

	 //public static admanager instance;
	 //private void Awake()
	 //{
		//  if (instance != null && instance != this)
		//  {
		//	   Destroy(gameObject);
		//	   return;
		//  }
		//  instance = this;
		//  DontDestroyOnLoad(this);
	 //}
	 //public bool AdmobPriorityInter, UnityPriorityInter, AdmobPriorityRewarded, UnityPriorityRewarded;
	 //// Start is called before the first frame update
	 //void Start()
	 //{
		//  //AdmobAds.instance.SetUserConsent(true);
		//  //AdmobAds.instance.SetCCPAConsent(true);
		//  AdmobAds.instance.requestInterstital();
		//  AdmobAds.instance.loadRewardVideo();

	 //}
	 //void ShowUnityBannerAd()
	 //{
		//  StartCoroutine(AdmobAds.instance.ShowBannerWhenInitialized());

	 //}
	 //public void ShowInterstitialAd()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//  {
		//	   if (AdmobPriorityInter)
		//	   {
		//			if (AdmobAds.instance.interstitial.IsLoaded())
		//			{
		//				 showVideoAd(); // admob inter ad

		//			}
		//			else
		//			{
		//				 AdmobAds.instance.showUnityInterstitialAd();
		//			}
		//	   }
		//	   else if (UnityPriorityInter)
		//	   {
		//			if (Advertisement.IsReady())
		//			{

		//				 AdmobAds.instance.showUnityInterstitialAd();
		//			}
		//			else
		//			{

		//				 showVideoAd();
		//			}

		//	   }
		//  }
	 //}
	 //public void ShowRewardedVideoAd(int i)
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//  {
		//	   PlayerPrefs.SetInt("VideoShownOnce", 0);
		//	   PlayerPrefs.SetInt("RewardKey", i);
		//	   if (UnityPriorityRewarded)
		//	   {
		//			if (Advertisement.IsReady("rewardedVideo"))
		//			{
		//				 AdmobAds.instance.ShowUnityRewardedVideo();

		//			}
		//			else
		//			{

		//				 showRewardedVideoAd();
		//			}

		//	   }
		//	   else if (AdmobPriorityRewarded)
		//	   {
		//			if (AdmobAds.instance.rewardedAd.IsLoaded())
		//			{
		//				 showRewardedVideoAd();

		//			}
		//			else
		//			{
		//				 AdmobAds.instance.ShowUnityRewardedVideo();
		//			}

		//	   }
		//  }
		//  //AdmobAds.instance.OnUnityAdsDidFinish("rewardedVideo", ShowResult showResult);
	 //}
	 //public void showbannercentreUp()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.reqBannerAdCentreUp();
	 //}
	 //public void showbannerbottomLeft()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.reqBannerAdBottomLeft();
	 //}
	 //public void showbannerbottomRight()
	 //{

		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.reqBannerAdBottomRight();
	 //}
	 //public void showbannerTopRight()
	 //{

		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.reqBannerAdTopRight();
	 //}
	 //public void showbannerTopLeft()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.reqBannerAdTopLeft();
	 //}
	 //public void showBoxBanner(int i)
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//  {
		//	   if (i == 0)
		//			AdmobAds.instance.boxbannerpos = BannerBoxPos.CenterLeft;
		//	   if (i == 1)
		//			AdmobAds.instance.boxbannerpos = BannerBoxPos.CenterRight;
		//	   if (i == 2)
		//			AdmobAds.instance.boxbannerpos = BannerBoxPos.Top;
		//	   if (i == 3)
		//			AdmobAds.instance.boxbannerpos = BannerBoxPos.Bottom;
		//	   if (i == 4)
		//			AdmobAds.instance.boxbannerpos = BannerBoxPos.bottomleft;
		//	   if (i == 5)
		//			AdmobAds.instance.boxbannerpos = BannerBoxPos.bottomRight;
		//	   if (i == 6)
		//			AdmobAds.instance.boxbannerpos = BannerBoxPos.TopLeft;
		//	   if (i == 7)
		//			AdmobAds.instance.boxbannerpos = BannerBoxPos.TopRight;

		//	   AdmobAds.instance.reqBannerAdBox();
		//  }
	 //}
	 //public void hideBottomLeftBanner()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.hideBannerBottomLeft();
	 //}
	 //public void hideBottomRightBanner()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.hideBannerBottomRight();
	 //}
	 //public void hideTopLeftBanner()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.hideBannerTopLeft();
	 //}
	 //public void hideTopRightBanner()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.hideBannerTopRight();
	 //}
	 //public void hideBoxBanner()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.hidereqBannerAdBox();
	 //}
	 //public void hideCentreUpBanner()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.hideBannerCentreUp();
	 //}
	 //void showVideoAd()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.ShowAdmobInterstitialAd();

	 //}
	 //void showRewardedVideoAd()
	 //{
		//  if (Application.internetReachability != NetworkReachability.NotReachable)
		//	   AdmobAds.instance.showAdmobRewardedVideoAd();
		//  //AdmobAds.instance.HandleUserEarnedReward();

	 //}


	 public void rewardOfRewardedVideo()
	 {
		  if (PlayerPrefs.GetInt("VideoShownOnce") == 0)
		  {
			   if (PlayerPrefs.GetInt("RewardKey") == 0)
			   {
					GameConfiguration.setTotalCash(GameConfiguration.getTotalCash() + 1000);
					MainMenuController.instance.UpdateCash();

			   }
			   if (PlayerPrefs.GetInt("RewardKey") == 1)
			   {
					GameConfiguration.setTotalCash(GameConfiguration.getTotalCash() + 1000);
					CashBundlesScreen.Instance.UpdateCash();

			   }
			   if (PlayerPrefs.GetInt("RewardKey") == 2)
			   {
					//give extra day

			   }
			   if (PlayerPrefs.GetInt("RewardKey") == 3)
			   {
					//give extra day

			   }
			   if (PlayerPrefs.GetInt("RewardKey") == 4)
			   {
					//give extra day

			   }
			   if (PlayerPrefs.GetInt("RewardKey") == 5)
			   {
					//give extra day

			   }
			   PlayerPrefs.SetInt("VideoShownOnce", 1);
		  }

	 }

}
