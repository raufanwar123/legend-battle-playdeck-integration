using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVNativePopup{
	public class GVNativeAlertAndroid {
		static AndroidJavaObject gvPopup;

		public static void showAlertBox(string msg){
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");

			gvPopup = new AndroidJavaObject ("com.gv.farhan.gvnativepopup.GVNativePopup");
			gvPopup.Call ("showAlertBox", context, msg);
		}
	}
}