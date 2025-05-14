using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType
{
    SimpleMode,
    WaveMode,
    DestinationMode
}

public class LevelController : MonoBehaviour {

    public static LevelController instance;

    public LevelType levelType;
    public Transform InitialPlayerPos;

    public string LevelObjectiveString;

    public int totalNumOfEnemies = 0;

    public GameObject[] enemies;

    public int levelCompletionReward = 0;

    public float timeForTHisLevel = 0;
    //public bool StartTime = false;
    public bool isLevelCompleted = false;

    public GameObject WaveManagerObj;
    public GameObject DestinationPoint;

    public bool hasCutScene = false;
    public GameObject cutSceneObj;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        totalNumOfEnemies = enemies.Length;
        GameController.instance.PlayerCharacter.transform.position = InitialPlayerPos.position;
        GameController.instance.PlayerCharacter.transform.rotation = Quaternion.Euler(InitialPlayerPos.localRotation.x, InitialPlayerPos.localRotation.y, InitialPlayerPos.localRotation.z);
        CheckForLevelType();
        if (UIController.instance)
        {
            UIController.instance.EnemyCountUpdate(totalNumOfEnemies);
        }
        //UpdateTimeForThisLevel();
    }

    //private void FixedUpdate()
    //{
        //if (StartTime && isLevelCompleted == false)
        //{
        //    UpdateTimeForThisLevel();
        //}
    //}
    public void UpdateTimeForThisLevel()
    {
        if (timeForTHisLevel > 0.5f)
        {
            timeForTHisLevel -= Time.fixedDeltaTime;

            string minutes = Mathf.Floor(timeForTHisLevel / 60).ToString("00");
            string seconds = (timeForTHisLevel % 60).ToString("00");

            UIController.instance.timeTxt.text = string.Format("{0}:{1}", minutes, seconds);
        }
        else
        {
            GameController.instance.ShowLevelFailedPanel();
        }
    }

    public void UpdateEnemyCount()
    {
        if (GameController.instance)
        {
            GameController.instance.enemyKilled++;
        }
        totalNumOfEnemies--;

        if (UIController.instance)
        {
            UIController.instance.EnemyCountUpdate(totalNumOfEnemies);
        }

        if (totalNumOfEnemies <= 0)
        {
            if (levelType == LevelType.SimpleMode)
            {
                print("Level Complete");
                if (GameController.instance)
                {
                    GameController.instance.SetLevelComplete();
                }
            }
            else if (levelType == LevelType.DestinationMode)
            {
                DestinationPoint.SetActive(true);
            }
        }
    }

    private void CheckForLevelType()
    {
        if (levelType == LevelType.WaveMode)
        {
            if (UIController.instance)
            {
                UIController.instance.SHowEnemyCountObj(false);
            }
        }
        else
        {
            if (UIController.instance)
            {
                UIController.instance.SHowEnemyCountObj(true);
            }
        }
    }
}
