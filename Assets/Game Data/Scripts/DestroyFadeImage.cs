using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DestroyFadeImage : MonoBehaviour {
	#region
	public bool flag;
	#endregion
	// Use this for initialization
	void Start () 
	{
		flag = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (gameObject.GetComponent<Image> ().color.a <= 0) 
		{
			if (flag) 
			{
				flag = false;
				gameObject.SetActive (false);
			}
		}
	}
}
