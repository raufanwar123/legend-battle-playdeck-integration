using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MyNPCWaveManager : MonoBehaviour
{
    [Serializable]
    public class EnvironmentLogic
    {
        public string level_Name;
        public int total_Levels;
        public int enemies_PerLevel_AtStart;
        public int[] enemies_ToBe_Killed_PerLevel;
        public Transform spawnPoints_Parent, enemyPatrolPointsParent;
    }


    public static MyNPCWaveManager instance;

    [Header("Environemts")]
    public GameObject environment;
    //public GameObject singlePlayerEnvironment;
    //public GameObject multiPlayerEnvironment;
    //public Material[] SkyBoxes;
    public bool _testenv;
    [Range(0, 4)]
    public int _envno = 0;

    public bool _testlvl;
    [Range(0, 20)]
    public int _levlno = 0;

    [Header("Player Settings")]
    public Transform fps_Player;
    //Refrence of SmoothMouseLook Script To Rotate player camera in the direction of playerSpawnPoint
    public SmoothMouseLook playerCameraSmoothMouseLook;

    [Header("GiftBoxes")]
    [Space(10f)]
    public GameObject[] giftBoxes;



    [Header("NPCRegistry")]
    public NPCRegistry npCRegistry;

    [Header("Environment Levels Logic")]
    [SerializeField]
    public EnvironmentLogic[] env_Enem_Player_SP_Array;
    [SerializeField]
    [HideInInspector]
    public EnvironmentLogic activeEnvironment_Logic;

    [Header("Enemies Types")]
    public GameObject[] typesOfNPC;

    [Header("Variables")]
    public Text warmUpTimeText;
    public float time_BTW_NPC_Spawn;
    //public GameObject flag;

    [Header("Stats")]
    public int totalEnemies_To_Be_Killed; //is decremented in NPCRegistry UnregisterNPC Method
    public int totalEnemies_Spawned;      //how many enemies spawned
    public int player_SpawnPoint_Number;
    public Transform playerTransformDebug;
    public bool isFlagCaptured;

    [Header("Team Members")]
    public GameObject[] teamMembers;
    [HideInInspector]
    public int currentTeamMemmber;
    public Sprite[] teamMemberSprites;
    public Image companionImage;
    public GameObject companionImageObject;
    public Image companionHealthBar;
    public GameObject[] danceCompanion;
    private WaitForSeconds wfs, wfs_Enemies_Count_Check;
    [HideInInspector]
    public int current_Environment_Number = 0, current_Level_Number = 0;

    int tempCounter;
    int random = 0;
    public PausePanell pause;
    private void Awake()
    {
        Singleton();
        if (GVSoundManager.Instance)
        {
            GVSoundManager.Instance.StopBgMusic();

        }
    }



    private void Start()
    {
        pause.CheckSensivity();
        Singleton();
        Initialization();
        LevelInitialization();
        PlayerInitialization();
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
        {
            currentTeamMemmber = GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_TEAM_MEMBER);
            companionImage.sprite = teamMemberSprites[currentTeamMemmber];
            StartCoroutine( SpwanTeamMember(/*1,*/1));
            CheckGiftBoxes();
        }
        else
        {
            StartCoroutine(SpwanTeamMember(/*1,*/ activeEnvironment_Logic.enemies_PerLevel_AtStart));
        }
    }

    private void Singleton()
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

    private void Initialization()
    {
        wfs = new WaitForSeconds(time_BTW_NPC_Spawn);
        wfs_Enemies_Count_Check = new WaitForSeconds(1f);

        if (!_testenv)
        {
            current_Environment_Number = GameConfiguration.GetSelectedEnvironment();

        }
        else
        {
            current_Environment_Number = _envno;
        }

        if (!_testlvl)
        {
            current_Level_Number = GameConfiguration.GetSelectedLevel();
        }
        else
        {
            current_Level_Number = _levlno;
        }
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
            companionImageObject.SetActive(true);
        else
            companionImageObject.SetActive(false);
        environment.SetActive(true);
        activeEnvironment_Logic = env_Enem_Player_SP_Array[0];
        totalEnemies_To_Be_Killed = activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number];
        

    }
    Transform t;
    public void PlayerInitialization()
    {
        // Comment by waseem for team member
        int randomSpawnPoint = UnityEngine.Random.Range(0, activeEnvironment_Logic.spawnPoints_Parent.childCount);

        player_SpawnPoint_Number = randomSpawnPoint;
        
        t = activeEnvironment_Logic.spawnPoints_Parent.GetChild(player_SpawnPoint_Number);
        //t.parent = null;
        playerTransformDebug = t;

        fps_Player.transform.position = t.position;
        playerCameraSmoothMouseLook.originalRotation =
        Quaternion.Euler(t.eulerAngles.x, t.eulerAngles.y, 0.0f);
        
    }




    void CheckGiftBoxes()
    {
        bool state;
        state = current_Level_Number % 3 == 0 ? true : false;

        foreach (GameObject g in giftBoxes)
        {
            if (g)
                g.SetActive(state);
        }
    }


    int randomCompanionPoint;
    int companion_SpawnPoint_Number;
    Transform companion_Transform;
    public IEnumerator SpwanTeamMember(int numberOfCompanion)
    {
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < numberOfCompanion; i++)
        {
            GameObject memmber;
            randomCompanionPoint = UnityEngine.Random.Range(0, activeEnvironment_Logic.spawnPoints_Parent.childCount);
            companion_SpawnPoint_Number = randomCompanionPoint;
            
            companion_Transform = activeEnvironment_Logic.spawnPoints_Parent.GetChild(companion_SpawnPoint_Number);
            if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
            {
                memmber = Instantiate(teamMembers[currentTeamMemmber], companion_Transform.transform.position, Quaternion.identity);
            }
            else
            {
                memmber = Instantiate(typesOfNPC[0], companion_Transform.transform.position, Quaternion.identity);
            }
            
                companionHealthBar.fillAmount = 1;
            
            if (memmber.GetComponent<AI>())
            {
                AI tm = memmber.GetComponent<AI>();
                tm.huntPlayer = false;
                tm.walkOnPatrol = true;
                //tm.factionNum = setFaction;

                int wp = 1;
                tm.waypointGroup = activeEnvironment_Logic.enemyPatrolPointsParent.GetChild(wp).gameObject.GetComponent<WaypointGroup>();
            }
            yield return wfs;
        }
    }
    Coroutine coroutine;
    private void LevelInitialization()
    {
        coroutine = StartCoroutine(SpawnEnemies(activeEnvironment_Logic.enemies_PerLevel_AtStart));
    }
    
    public int friends_PerLevel;
    public int totalFriendsSpwan;
   public IEnumerator SpawnEnemies(int enemies_PerLevel_AtStart)
    {
        yield return new WaitForSeconds(3.0f); //imran ---- Wait for player to be spawn first so enemy could not be spawn by player

        for (int i = 0; i < enemies_PerLevel_AtStart; i++)
        {
            int randomSpawnPoint = UnityEngine.Random.Range(0, activeEnvironment_Logic.spawnPoints_Parent.childCount);

            
            while (check_IF_SpawnPointHasPlayer(randomSpawnPoint)) //Imran .... to avoid enemy and playeroverlaping
            {
                randomSpawnPoint = UnityEngine.Random.Range(0, activeEnvironment_Logic.spawnPoints_Parent.childCount);
            }

            
            GameObject ai = Instantiate(typesOfNPC[1], activeEnvironment_Logic.spawnPoints_Parent.GetChild(randomSpawnPoint).position, Quaternion.identity);
            if (ai.GetComponent<AI>())
            {
                tempCounter = 2;
                AI e = ai.GetComponent<AI>();
                if (tempCounter == 0)
                {
                    e.standWatch = false;
                    e.huntPlayer = true;
                    e.walkOnPatrol = false;
                    int wp = 1;
                    e.waypointGroup = activeEnvironment_Logic.enemyPatrolPointsParent.GetChild(wp).gameObject.GetComponent<WaypointGroup>();
                }
                if (tempCounter == 1)
                {
                    e.standWatch = true;// waseem change it to false it was true
                    e.huntPlayer = false;
                    e.walkOnPatrol = false; //true

                    int wp = 1; //UnityEngine.Random.Range(0, activeEnvironment_Logic.enemyPatrolPointsParent.childCount); //junaid added following lines
                    e.waypointGroup = activeEnvironment_Logic.enemyPatrolPointsParent.GetChild(wp).gameObject.GetComponent<WaypointGroup>();
                }
                if (tempCounter == 2)
                {
                    int wp = UnityEngine.Random.Range(0, activeEnvironment_Logic.enemyPatrolPointsParent.childCount);
                    e.waypointGroup = activeEnvironment_Logic.enemyPatrolPointsParent.GetChild(wp).gameObject.GetComponent<WaypointGroup>();
                    e.walkOnPatrol = true;
                    e.standWatch = false;
                    e.huntPlayer = false;
                }



                tempCounter++;
                if (tempCounter == 3)
                {
                    tempCounter = 0;// tempCounter = 0; //waseem
                }
            }
            totalEnemies_Spawned += 1;
//            Debug.Log("Total Enemy Spwaned : " + totalEnemies_Spawned);
            yield return wfs;
        }

    }

    
    //Called in NPCRegistry UnregisterNPC Method
    bool check_IF_SpawnPointHasPlayer(int pointIndx)
    {
        NPCSpawnPoint spwnPntScript = activeEnvironment_Logic.spawnPoints_Parent.GetChild(pointIndx).GetComponent<NPCSpawnPoint>();
        return spwnPntScript.HasPlayer;
    }
    public void Check_If_Enemy_Killed()
    {
      //  StartCoroutine(EnemyDownSoundCR());
        if (totalEnemies_Spawned < activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number])
        {
            StopCoroutine(coroutine);
            coroutine = null;
            coroutine = StartCoroutine(SpawnEnemies(1));
        }
    }
    
    IEnumerator EnemyDownSoundCR()
    {
        yield return new WaitForSeconds(2);
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlaySound("iSR-Enemy_Down");

    }
    public void Check_If_All_Enemies_Dead()
    {
        if (totalEnemies_To_Be_Killed == 0 && !isFlagCaptured)
             {
           
            if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
            {
                GameStat.instance.isGameComplete = true;
                //StartCoroutine(VictoryDance());
                LevelComplete();
            }
            isFlagCaptured = true;
            int currentCash = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
            currentCash = currentCash + SetGameCompleteTexts();
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, currentCash);
        }
    }

    public void OnAIDie(int factionNo)
    {
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 1)
        {
            if (min > 0 || sec > 0)
            {
                if (factionNo == 1)
                {
                    GameStat.instance.oponentKills.text = " " + ++GameStat.oponentsPoints;
                    StartCoroutine( SpwanTeamMember(1));
                }
                else
                {
                  //  StartCoroutine(EnemyDownSoundCR());
                    GameStat.instance.myKills.text = " " + ++GameStat.myPoints;
                    StartCoroutine(SpawnEnemies(1));
                }
            }

            if (GameStat.oponentsPoints >= 30) //junaid we can increase the number of points
            {
                GameStat.instance.MultiplayerVictory("Defeat");
                GameStat.instance.isGameOver = true;
            }
            else if (GameStat.myPoints >= 30)
            {
                GameStat.instance.MultiplayerVictory("Victory");
                GameStat.instance.isGameComplete = true;
            }
        }
        else //SinglePlayerMode
        {
            if (factionNo == 1) //friend
                StartCoroutine( SpwanTeamMember(1));

            else //enemy //Imran code
            {
                totalEnemies_To_Be_Killed -= 1;
               // StartCoroutine(EnemyDownSoundCR());
                if (totalEnemies_Spawned < activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number])
                {
                    StartCoroutine( SpawnEnemies(1));
                }
            }
        }
    }

    public void LevelComplete()
    {
        StartCoroutine(VictoryDance());
    }
    IEnumerator VictoryDance()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameStat.instance)
        {
            GameStat.instance.GamePlayUI.SetActive(false);
            GameStat.instance.miniMapCanvas.SetActive(false);
           // GameStat.instance.victoryDancePanel.SetActive(true);
            //danceCompanion[currentTeamMemmber].SetActive(true);
        }
        yield return new WaitForSeconds(0.1f) ;
        GameStat.instance.miniMapCanvas.SetActive(true);
        GameStat.instance.GamePlayUI.SetActive(true);
        GameStat.instance.victoryDancePanel.SetActive(false);
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
            GameStat.instance.CompleteFun();
    }
    IEnumerator Check_Enemies_Count_After_Interval()
    {
        yield return wfs_Enemies_Count_Check;
        //Check_If_All_Enemies_Dead();
        StartCoroutine(Check_Enemies_Count_After_Interval());
    }
    [Header("Game Stats On Complete and Failed")]
    public Text TotalCash;
    public Text HeadShot, Timeremain, totalEnemykilled, NoofCollectcoinpack, Coincollected, Coinheadshot, Cointime, Coinenemykilled;
    public int CoinsCollected = 0;
    public float min = 8;
    public float sec = 40;
    public int secd = 0;

    public void GameFailrComplete()
    {
        int currentCash = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
        currentCash = currentCash + SetGameCompleteTexts();
        GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, currentCash);
    }

    public int SetGameCompleteTexts()
    {
        int totalcash = 0;

        HeadShot.text = EnemyCounter.instance.HeadSHotCOunter.ToString();
        Coinheadshot.text = (EnemyCounter.instance.HeadSHotCOunter * 50).ToString();

        Coincollected.text = (CoinsCollected * 50).ToString();
        NoofCollectcoinpack.text = CoinsCollected + "";
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 1) //single player
        {
            totalEnemykilled.text = (GameStat.myPoints).ToString();
            Coinenemykilled.text = (GameStat.myPoints * 200).ToString();
            totalcash += GameStat.myPoints * 200;
            //totalEnemykilled.text = (activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies).ToString();
            //Coinenemykilled.text = ((activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies) * 200) + "";
            //totalcash += ((activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies) * 200);
        }
        else
        {

            totalEnemykilled.text = (activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies).ToString();
            Coinenemykilled.text = ((activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies) * 200) + "";
            totalcash += ((activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies) * 200);
            //totalEnemykilled.text = (activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies).ToString();
            //Coinenemykilled.text = ((activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies) * 200) + "";
            //totalcash += ((activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[current_Level_Number] - EnemyCounter.instance.RemainingEnemies) * 200);
        }
        secd = (int)sec;

        Timeremain.text = (int)min + ":" + secd;
        Cointime.text = ((min * 60) + secd).ToString();
        totalcash += (int)((min * 60) + secd);
        TotalCash.text = (totalcash + (CoinsCollected * 50) + (EnemyCounter.instance.HeadSHotCOunter * 50)).ToString();
        return totalcash;
    }
    int totalcash = 0;
    int currentCash;
    public void AddMoreCoins(int amount)
    {
        Debug.Log("How much Coins Added  :  " + totalcash / 100 * 15);
        totalcash = totalcash + (totalcash / 100 * amount);
        TotalCash.text = totalcash.ToString();
        currentCash = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey);
        totalcash = totalcash + currentCash;
        GameConfiguration.SetIntegerKeyValue(GameConfiguration.CashKey, totalcash);
    }

    public RectTransform timerTransform;
    public bool useTime = false;
    public Text gameplayTimeText;
    public void TimeCounterStart()
    {
        useTime = true;
    }
    private void Update()
    {
        if (useTime)
        {
            gameplayTimeText.text = min.ToString("00") + ":" + sec.ToString("00");
            sec -= Time.deltaTime;
            secd = (int)sec;
            if (sec < 0 && min > 0)
            {
                min--;
                sec = 60;
            }
            else if (sec <= 0 && min <= 0)
            {
                if (GameStat.myPoints > GameStat.oponentsPoints)
                {
                    GameStat.instance.MultiplayerVictory("Victory");
                    GameStat.instance.isGameComplete = true;
                }
                else
                {
                    GameStat.instance.MultiplayerVictory("Defeat");
                    GameStat.instance.FailFun("Time");
                    
                    GameStat.instance.isGameOver = true;

                }
                useTime = false;
            }

        }
    }
}
