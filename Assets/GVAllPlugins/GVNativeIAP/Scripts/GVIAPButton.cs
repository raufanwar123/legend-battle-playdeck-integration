using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GVNativeIAP{
    public enum ProductType
    {
        CONSUMEABLE= 0,
        NONCONSUMABLE =1,
        SUBSCRIPTION =2
    };
	[System.Serializable]
	public class GVIAPCustomEventButton : UnityEvent {}

	public class GVIAPButton : MonoBehaviour {

		public string productId="Product ID";
		public ProductType _productType = ProductType.CONSUMEABLE;
		public string _productPrice = "Loading...";
		public bool isPriceLoaded = false;
		public Text _priceTxt;

		[Space(20)]
		public GVIAPCustomEventButton onPurchaseSuccess;
		public GVIAPCustomEventButton onPurchaseFail;

		// Use this for initialization
		void Start () {

			GVIAPListener.OnProductPriceSuccess += GVIAPListener_OnProductPriceSuccess;
            _priceTxt.text = "Loading...";
			StartCoroutine("LoadItemPriceCR");
		}

        private void OnDestroy()
        {
			GVIAPListener.OnProductPriceSuccess -= GVIAPListener_OnProductPriceSuccess;
		}

        void registerCallbacks(){
			GVIAPListener.purchaseSuccess += callPurchaseSuccess;
			GVIAPListener.purchaseFail += callPurchaseFail;
            
		}

        

        void deregisterCallbacks(){
			GVIAPListener.purchaseSuccess -= callPurchaseSuccess;
			GVIAPListener.purchaseFail -= callPurchaseFail;
		}

		public void purchaseProduct(){
			registerCallbacks ();
			//UnityPurchaser.Instance.BuyProductID(productId);
        }
        int GetProdcutTypeIntfromEnum(ProductType type)
		{
            switch (type)
            {
				case ProductType.CONSUMEABLE:
                    return 0;
                case ProductType.NONCONSUMABLE:
					return 1;
				case ProductType.SUBSCRIPTION:
                    return 2;
                default:
					return 0;
			}
                
        }

		void callPurchaseSuccess(string prodId){
			if (prodId == productId) {
				deregisterCallbacks ();
				onPurchaseSuccess.Invoke ();
			}
		}

		void callPurchaseFail(string errorMsg){
			deregisterCallbacks ();
			onPurchaseFail.Invoke ();
		}

		

		private void GVIAPListener_OnProductPriceSuccess(string prodID, string prodPrice)
		{
			if (prodID == productId)
			{
				deregisterCallbacks();
				
				GVLogsManager.instance.DebugLog(this, "OnProductPriceSuccess Product ID: " + productId + " Price: " + prodPrice);
				_productPrice = prodPrice;
				isPriceLoaded = true;
				_priceTxt.text = prodPrice;
			}
		}

        IEnumerator LoadItemPriceCR()
        {
			yield return new WaitForSecondsRealtime(5);
			//UnityPurchaser.Instance.getProductPrice(productId);

			//GVIAPAndroid.getInstance().getProductPrice(productId);
		}
        
	}
}