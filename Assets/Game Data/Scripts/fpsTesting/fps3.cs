//using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class fps3 : MonoBehaviour 
{

	string label = "";
	float count;

	public Rect recc =new Rect (5, 40, 100, 25);


	IEnumerator Start ()
	{
		GUI.depth = 2;
		while (true) 
		{
			if (Time.timeScale == 1) 
			{
				yield return new WaitForSeconds (0.1f);
				count = (1 / Time.deltaTime);
				label = "FPS :" + (Mathf.Round (count));
			}

			else 
			{
				label = "Pause";
			}

			yield return new WaitForSeconds (0.5f);
		}

	}

	public GUIStyle gui1;

	void OnGUI ()
	{
		//GUI.Label (new Rect (5, 40, 100, 25), label);
		GUI.Label(recc ,label ,gui1);
	}
}