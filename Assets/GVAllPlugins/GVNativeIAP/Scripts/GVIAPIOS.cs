using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace GVNativeIAP{
	public class GVIAPIOS {
#if UNITY_IOS
        [DllImport ("__Internal")]
		private static extern void restorePurchase();

		[DllImport ("__Internal")]
		private static extern void purchaseProduct(string productId);

		public static void restorePurchases (){
			restorePurchase();
		}

		public static void purchaseThisProduct (string productId){
			purchaseProduct(productId);
		}
#endif
	}
}