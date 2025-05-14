using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimationOnStart : MonoBehaviour {
	public string AnimationName;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Animator> ().SetBool (AnimationName, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
