using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {
    public static LevelSettings instance;
	public GameObject [] Levels;
	public Transform[] LevelPositions;
	public Transform MyPlayer;
	int SelectedLevel;
    public bool testlevel;

    [Range(0,10)]
    public int Currentlevel;

    void SingleTon()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Use this for initialization
    void Awake()
	{
        
        if (PlayerPrefs.GetString("InfiniteMode") == "No")
        {
            print("Gun Number  " + PlayerPrefs.GetInt("Selected_Gun"));
            SelectedLevel = PlayerPrefs.GetInt("Level_Num");
#if UNITY_EDITOR
            if (testlevel)
            {
                SelectedLevel = Currentlevel;
                PlayerPrefs.SetInt("Level_Num", SelectedLevel);
            }
#endif
            //Levels[SelectedLevel].SetActive(true);
            //LevelPositions[SelectedLevel].transform.parent = null;
        }
	}
    void Start()
    {
        SingleTon();
        if (PlayerPrefs.GetString("InfiniteMode") == "No")
        {
            MyPlayer.transform.localPosition = LevelPositions[SelectedLevel].localPosition;
            MyPlayer.transform.localEulerAngles = LevelPositions[SelectedLevel].localEulerAngles;
        }
        else if (PlayerPrefs.GetString("InfiniteMode") == "Yes")
        {
            int random = Random.Range(0, LevelPositions.Length);
            LevelPositions[random].transform.parent = null;
            Debug.Log("Random: " + random);
            MyPlayer.transform.localPosition = LevelPositions[random].localPosition;
            MyPlayer.transform.localEulerAngles = LevelPositions[random].localEulerAngles;
        }
    }
	
	
}
