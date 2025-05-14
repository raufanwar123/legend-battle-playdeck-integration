using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GVNativePopup_Example : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void showPopup(){
		GVNativePopup.GVNativeAlert.showAlertBox ("This is testing message from unity");
	}
}
