using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowScore : MonoBehaviour {
	public Text MenuScore, LevelScore, GunScore;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		int Score=GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
		MenuScore.text = Score.ToString ();
		//LevelScore.text = Score.ToString ();
		GunScore.text = Score.ToString ();

	}


}
