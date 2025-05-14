using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GVNativeIAP{
	[System.Serializable]
	public class GVIAPCustomEventRestore : UnityEvent <IAPRestoreIDs>{}

	public class GVIAPRestore : MonoBehaviour {

		public GVIAPCustomEventRestore onRestoreFetch;

		// Use this for initialization
		void Start () {
			
		}

		void OnEnable()
		{
			GVIAPListener.purchaseRestore += callOnRestoreFetch;
		}


		void OnDisable()
		{
			GVIAPListener.purchaseRestore -= callOnRestoreFetch;
		}

		public void fetchRestoreIds(){
			#if !UNITY_EDITOR
				#if UNITY_IOS
					GVIAPIOS.restorePurchases ();
				#elif UNITY_ANDROID
					GVIAPAndroid.getInstance().restorePurchases();
				#endif
			#endif
		}

		public void callOnRestoreFetch(IAPRestoreIDs restoreIds){
			onRestoreFetch.Invoke (restoreIds);
		}
	}
}