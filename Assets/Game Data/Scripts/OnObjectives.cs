using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnObjectives : MonoBehaviour {
	public GameObject ObjectivePanel;
	// Use this for initialization

	void MyFun()
	{
		TopBarGameplay.Instance.ShowTopBar();
		//TopBarGameplay.Instance.ShowExitBtn();
		ObjectivePanel.SetActive (true);
	//	Time.timeScale = 0;
	}

	public void OkButtonWork()
	{
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlayBtnClickSound();

        StartCoroutine( PlayStartSFX());

		ObjectivePanel.SetActive (false);
		Time.timeScale = 1;
        MyNPCWaveManager.instance.TimeCounterStart();
        
		TopBarGameplay.Instance.HideTopBar();
        GameStat.instance.CheckAutoShoot();
		  //admanager.instance.showbannercentreUp();
	}

    IEnumerator PlayStartSFX()
    {
        yield return new WaitForSeconds(0.5f);
        //if (GVSoundManager.Instance)
        //{
        //    GVSoundManager.Instance.PlayBGMusic("GameplayBG");
        //}
		if (GVSoundManager.Instance)
			GVSoundManager.Instance.PlaySound("iSR-Good_Luck");
        yield return new WaitForSeconds(1f);
		if (GVSoundManager.Instance)
			GVSoundManager.Instance.PlaySound("iSR-Go_Go_Go");


    }
}
