using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVNativePopup{
	public class GVNativeAlert {

		public static void showAlertBox(string msg){
			#if !UNITY_EDITOR
				#if UNITY_IOS
				
				#elif UNITY_ANDROID
					GVNativeAlertAndroid.showAlertBox(msg);
				#endif
			#endif
		}

	}
}