using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneScript : MonoBehaviour {
    public static CutSceneScript instance;

	public GameObject CutSceneObject,GamePlayUI;
	public GameObject[] Camera1OfLevels;
	public GameObject[] Camera2OfLevels;
	public GameObject[] LevelCutScenes;
	public float[] TimeToOnSecondCamera;
	public float[] TimeToOnGamePlayObjects;
	public float RealTime;
	public float RealTime2;
	public GameObject FadeImage;
	public GameObject FadeImage2;
	int SelectedLevel;
	public GameObject FireButton;
	public GameObject LevelAssetGroup;

    public bool OnCutscene;
    public GameObject BankCutscene;
    public float CutTime;


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

    void Start () {
        SingleTon();
		SelectedLevel= PlayerPrefs.GetInt ("Level_Num");
		//RealTime=TimeToOnSecondCamera [SelectedLevel];
		//RealTime2=TimeToOnGamePlayObjects [SelectedLevel];
		//FadeImage.SetActive (true);
		//Invoke ("OnSecondCamera", RealTime);
		//Invoke ("OnGamePlay", RealTime2);
		//LevelCutScenes [SelectedLevel].SetActive (true);
        if (OnCutscene && SelectedLevel == 0)
        {
            StartCoroutine(PlayBankCut());
        }
        else
        OnGamePlay();

    }
	
	
    IEnumerator PlayBankCut()
    {
        if(BankCutscene)
        BankCutscene.SetActive(true);
        yield return new WaitForSeconds(CutTime);
        if (BankCutscene)
            BankCutscene.SetActive(false);
        OnGamePlay();
        print("CutSceneDone!");
    }
    public void OnSecondCamera()
	{
		Camera1OfLevels[SelectedLevel].SetActive (false);
		Camera2OfLevels[SelectedLevel].SetActive (true);
		FadeImage.SetActive (true);
	}

	public void OnGamePlay()
	{
        //if (CutSceneObject.activeInHierarchy)
        //{
        if (CutSceneObject)
            CutSceneObject.SetActive (false);
			GamePlayUI.SetActive (true);
			
			FadeImage2.SetActive (true);
			FireButton.transform.localScale = new Vector3 (1, 1, 1);
			LevelAssetGroup.SetActive (true);
		//}
	}
}
