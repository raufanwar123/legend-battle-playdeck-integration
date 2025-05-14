using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GVNativeIAP{
	public class GVIAPListener : MonoBehaviour {

		public delegate void PurchaseSuccess(string productId);
		public static event PurchaseSuccess purchaseSuccess;
		public delegate void PurchaseFail(string errorMsg);
		public static event PurchaseFail purchaseFail;
		public delegate void PurchaseRestore(IAPRestoreIDs iapRestoreIds);
		public static event PurchaseRestore purchaseRestore;
        public delegate void ProductPriceSuccess(string productID, string productPrice);
        public static event ProductPriceSuccess OnProductPriceSuccess;

        public void purchaseSuccessfull(string productId){
			purchaseSuccess (productId);
		}

		public void purchaseFailed(string error){
			purchaseFail (error);
		}

		public void purchaseRestored(string jsonData){
			IAPRestoreIDs myObject = JsonUtility.FromJson<IAPRestoreIDs>(jsonData);
			purchaseRestore (myObject);
		}

		[System.Serializable]
		public class ProductPriceInfo
		{
			public string productID;
			public string productPrice;

		}

		public void productPriceSuccessFull(string productID, string localizedPrice)
        {
			
			GVLogsManager.instance.DebugLog(this, "ProductPriceInfoClassData: " + productID);
			GVLogsManager.instance.DebugLog(this, "ProductPriceInfoClassData: " + localizedPrice);
			OnProductPriceSuccess(productID, localizedPrice);
        }

        void Start () {
            GVIAPAndroid.getInstance();
            DontDestroyOnLoad (gameObject);
		}
	}
}