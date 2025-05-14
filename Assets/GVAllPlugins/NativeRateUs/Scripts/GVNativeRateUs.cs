using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GVNativeRateUs {
	

	public static void openNativeRateUs(){
#if UNITY_ANDROID
			Application.OpenURL("market://details?id=" +Application.identifier);
#elif UNITY_IPHONE
       UnityEngine.iOS.Device.RequestStoreReview();
#endif
    }
}
