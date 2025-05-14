using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSomeTime : MonoBehaviour {
	bool flag;
	public float TimeToDestroy;
	// Use this for initialization
	void Start () 
	{
		Invoke ("MyFun", TimeToDestroy);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void MyFun()
	{
		Destroy (gameObject);
	}
}
