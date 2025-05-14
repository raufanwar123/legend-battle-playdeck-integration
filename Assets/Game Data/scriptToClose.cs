using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptToClose : MonoBehaviour 
{

	public float timeMax = 2.49f;
	public float currentTime = 1f;
    public bool USeEnable;
    void OnEnable()
    {
        if (!USeEnable)
        {
            Time.timeScale = 1f;
            currentTime = 0f;
            timeMax = 2.49f;
            gameObject.SetActive(true);
        }
        else
        {
            currentTime = 0f;
            Time.timeScale = 1f;

        }
    }


	
	void Update () 
	{
		currentTime += Time.deltaTime;

		if (currentTime > timeMax) 
		{
			
			closeFunction ();
		}
	}

	void closeFunction()
	{
		gameObject.SetActive (false);

	}
}
