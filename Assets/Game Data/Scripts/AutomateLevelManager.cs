using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutomateLevelManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform fps_Player;
    //Refrence of SmoothMouseLook Script To Rotate player camera in the direction of playerSpawnPoint
    public SmoothMouseLook playerCameraSmoothMouseLook;
    public Transform[] player_Spawn_Points;

    #region Levels Settings
    [Header("Levels Settings")]
    public GameObject[] types_OF_NPC;
    public Transform[] npc_Spawn_Points;
    public int[] total_Enemies_Per_Level;
    //npc damage to player according to level
    public float[] damageToPlayer;
    #endregion

    #region Gernade Settings
    public WeaponBehavior gernadeWeaaponBehaviour;
    public Text gernadeLeftText;
    #endregion

    #region Stats
    [Header("Stats")]
    public int levelNumber;
    public int remainingEnemies;

    [Range(0, 20)]
    public int Currentlevel;
    public int totalGernades;

    public float time_BTW_NPC_Spawns, time_Before_Start_Spawning;
    public bool isTestLevel, completeFlag;

    #endregion

    #region private variables
    WaitForSeconds wfs, wfs_Time_Before_Start_Spawning;
    #endregion

    private void Start()
    {
        Initialization();
        PlayerInitialization();
        LevelInitialization();
    }

    #region Initialization Methods
    void Initialization()
    {
        totalGernades = PlayerPrefs.GetInt("TotalGernades");
        gernadeWeaaponBehaviour.ammo = totalGernades;
        completeFlag = true;
        remainingEnemies = total_Enemies_Per_Level[levelNumber];
        if (PlayerPrefs.GetString("InfiniteMode") == "No")
        {
            levelNumber = PlayerPrefs.GetInt("Level_Num");
#if UNITY_EDITOR
            if (isTestLevel)
            {
                levelNumber = Currentlevel;
                PlayerPrefs.SetInt("Level_Num", levelNumber);
            }
#endif
        }
    }

    void PlayerInitialization()
    {
        int random = Random.Range(0, player_Spawn_Points.Length);
        fps_Player.transform.localPosition = player_Spawn_Points[random].localPosition;
        playerCameraSmoothMouseLook.originalRotation =
            Quaternion.Euler(player_Spawn_Points[levelNumber].eulerAngles.x, player_Spawn_Points[levelNumber].eulerAngles.y, 0.0f);
    }

    void LevelInitialization()
    {
        levelNumber = PlayerPrefs.GetInt("Level_Num");

        wfs = new WaitForSeconds(time_BTW_NPC_Spawns);
        wfs_Time_Before_Start_Spawning = new WaitForSeconds(time_Before_Start_Spawning);

        StartCoroutine(StartSpawning_NPC_Coroutine());
    }


    IEnumerator StartSpawning_NPC_Coroutine()
    {
        yield return wfs_Time_Before_Start_Spawning;

        for (int i = 0; i < total_Enemies_Per_Level[levelNumber]; i++)
        {
            int random = Random.Range(0, types_OF_NPC.Length);
            int randomSpawnPoint = Random.Range(0, npc_Spawn_Points.Length);

            GameObject ai = Instantiate(types_OF_NPC[random], npc_Spawn_Points[randomSpawnPoint].position, Quaternion.identity);
            ai.GetComponent<AI>().huntPlayer = true;
            ai.GetComponent<NPCAttack>().damage = damageToPlayer[levelNumber];

            yield return wfs;
        }
        Debug.Log("End Of Coroutine");
    }
    #endregion


}
