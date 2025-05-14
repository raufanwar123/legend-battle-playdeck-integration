using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class EnemyCounter : MonoBehaviour 
{
    public static EnemyCounter instance;

    public WeaponBehavior pdrPickup_Weapon/*, scriflePickUp_Weapon*/;

    
    public WeaponBehavior gernadeWeaaponBehaviour;
    public Text gernadeLeftText;

	public int[] LevelEnemies;
    public GameObject captureFlagInicator;
    public bool isDisableFlagIndicator;
    public float flagDisableTime = 7;

    public GameObject flagPrefab;

    public GameObject[] levelFlags;

	public int RemainingEnemies;
	public bool CompleteFlag;
	public Text RemainingText;
	int SelectedLevel;
    int totalEnemies;
    int totalGernade;

    public bool isMedicKit;
    public bool isPickUpGun;
    public int HeadSHotCOunter = 0;
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
	void Start () 
	{
        SingleTon();
        PlayerPrefs.SetString("InfiniteMode", "No");
        totalGernade = PlayerPrefs.GetInt("TotalGernades");
        gernadeWeaaponBehaviour.ammo = totalGernade;
        CompleteFlag = true;
		SelectedLevel = PlayerPrefs.GetInt ("Level_Num");
		RemainingEnemies = LevelEnemies [SelectedLevel];
        totalEnemies = RemainingEnemies;
	}

    void DisableAllFlags()
    {
        for (int i = 0; i < levelFlags.Length; i++)
        {
            levelFlags[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
        //CheckPlayerHealth();
        ShowGernadeMedicKitPanel();
        IsInfiniteMode();
        if (isPickUpGun)
            CheckPickUpGunsDetails();
    }

    public void CheckPickUpGunsDetails()
    {
        if (pdrPickup_Weapon.gameObject.activeInHierarchy)
        {
            if (pdrPickup_Weapon.bulletsLeft == 0 && pdrPickup_Weapon.ammo == 0)
            {
                pdrPickup_Weapon.haveWeapon = false;
                pdrPickup_Weapon.cycleSelect = false;
                CustomCharacterController.instance.SwitchUnlockedWeapons(8);
                isPickUpGun = false;
            }
            
        }
        //else if (scriflePickUp_Weapon.gameObject.activeInHierarchy)
        //{
        //    if (scriflePickUp_Weapon.bulletsLeft == 0 && scriflePickUp_Weapon.ammo == 0)
        //    {
        //        scriflePickUp_Weapon.haveWeapon = false;
        //        scriflePickUp_Weapon.cycleSelect = false;
        //        CustomCharacterController.instance.SwitchUnlockedWeapons(5);
        //        isPickUpGun = false;
        //    }
           
        //}
    }

    void ShowGernadeMedicKitPanel()
    {
        gernadeLeftText.text = gernadeWeaaponBehaviour.ammo.ToString();
        PlayerPrefs.SetInt("TotalGernades", gernadeWeaaponBehaviour.ammo);
        totalGernade = gernadeWeaaponBehaviour.ammo;
        if (ControlFreak2.CF2Input.GetButton("Throw Grenade"))
        {
            if (totalGernade != 0)
            {
                if (GVSoundManager.Instance)
                    GVSoundManager.Instance.PlaySound("fire in the hole");
            }
        }
        #region Comment
        //if (ControlFreak2.CF2Input.GetButton("Throw Grenade"))
        //{
        //    if (totalGernade <= 0 && !isMedicKit)
        //    {
        //        Debug.Log("Gernade Throw");
        //        if (GameStat.instance)
        //        {
        //            GameStat.instance.gernadeMedicKitPanel.SetActive(true);
        //            GameStat.instance.playerCanvas.SetActive(false);
        //            GameStat.instance.miniMapCanvas.SetActive(false);
        //            GameStat.instance.playerCrossHairImage.enabled = false;

        //            CutSceneScript.instance.Buttons.SetActive(false);
        //            if (LevelSettings.instance)
        //            {
        //                if (PlayerPrefs.GetString("InfiniteMode") == "No")
        //                {
        //                    LevelSettings.instance.Levels[SelectedLevel].SetActive(false);
        //                }
        //            }
        //            Time.timeScale = 0.001f;
        //        }
        //    }
        //}
        #endregion
    }

    void CheckPlayerHealth()
    {
        if (CustomCharacterController.instance)
        {
            if (CustomCharacterController.instance.fpSPlayer.hitPoints < 45)
            {
                if (!CustomCharacterController.instance.medicKitUIAnimator.enabled)
                {
                    CustomCharacterController.instance.medicKitUIAnimator.enabled = true;
                    CustomCharacterController.instance.medicKitImage.color = Color.red;
                }
            }
            else if (CustomCharacterController.instance.fpSPlayer.hitPoints > 45)
            {
                if (CustomCharacterController.instance.medicKitUIAnimator.enabled)
                {
                    CustomCharacterController.instance.medicKitUIAnimator.enabled = false;
                    CustomCharacterController.instance.medicKitImage.color = Color.white;
                    CustomCharacterController.instance.medicKitImage.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    void IsInfiniteMode()
    {
        if (PlayerPrefs.GetString("InfiniteMode") == "No")
        {
            RemainingEnemies = MyNPCWaveManager.instance.totalEnemies_To_Be_Killed;
            totalEnemies = MyNPCWaveManager.instance.activeEnvironment_Logic.enemies_ToBe_Killed_PerLevel[MyNPCWaveManager.instance.current_Level_Number];
            RemainingText.text = RemainingEnemies.ToString() + " / " + totalEnemies.ToString();

            //if (RemainingEnemies == 0)
            //{
            //    gameObject.GetComponent<GameStat>().CompleteFun();
            //}
            //if (CompleteFlag && RemainingEnemies == 0)
            //{
            //    levelFlags[SelectedLevel].SetActive(true);

            //    captureFlagInicator.SetActive(true);
            //    CompleteFlag = false;
            //    if (isDisableFlagIndicator)
            //    {
            //        StartCoroutine(DisbaleFlagIndicator());
            //    }
            //}
        }
        else if (PlayerPrefs.GetString("InfiniteMode") == "Yes")
        {
            if (RemainingEnemies == 0)
            {
                captureFlagInicator.SetActive(true);
                if (isDisableFlagIndicator)
                {
                    StartCoroutine(DisbaleFlagIndicator());
                }
            }
            RemainingText.text = RemainingEnemies.ToString();
        }
    }

    public void FlagCaptured()
    {
        //gameObject.GetComponent<GameStat>().CompleteFun();
    }
    public IEnumerator DisbaleFlagIndicator()
    {
        yield return new WaitForSeconds(flagDisableTime);
        captureFlagInicator.SetActive(false);
    }
}
