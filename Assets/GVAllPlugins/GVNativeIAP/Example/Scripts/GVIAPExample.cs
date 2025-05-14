using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GVNativeIAP;

public class GVIAPExample : MonoBehaviour {

	public Text responseText;
	
	int first=5;
	int second=10;

	// Use this for initialization
	void Start () {
        GVIAPListener.OnProductPriceSuccess += GVIAPListener_OnProductPriceSuccess;
        
	}

    private void GVIAPListener_OnProductPriceSuccess(string productID, string productPrice)
    {
		GVLogsManager.instance.DebugLog(this, "GVIAPExample: " + "GVIAPListener_OnProductPriceSuccess: " + productID);
		GVLogsManager.instance.DebugLog(this,"GVIAPExample: " + "GVIAPListener_OnProductPriceSuccess: " + productPrice);
    }

    public void callThisEventResponse(IAPRestoreIDs obj){
		responseText.text = obj.ToString ();
	}

	public void itemPurchaseSuccess(){
		responseText.text = "Item Purchase Successfully";
	}

	public void itemPurchaseFail(){
		responseText.text = "Fail To Purchase Item";
	}

    public void getItemPrice(string productID)
    {
        GVIAPAndroid.getInstance().getProductPrice(productID);
    }
}
