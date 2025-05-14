using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseEnemyCounter : MonoBehaviour {

	public void DecreaseCounter()
	{
		if (EnemyCounter.instance) 
		{
            if (PlayerPrefs.GetString("InfiniteMode") == "No")
            {
                EnemyCounter.instance.RemainingEnemies--;
            }
            else if (PlayerPrefs.GetString("InfiniteMode") == "Yes")
            {
                EnemyCounter.instance.RemainingEnemies++;
            }
        }
	}
}
