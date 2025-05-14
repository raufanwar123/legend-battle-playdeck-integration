using System.Collections;
using ControlFreak2;
using UnityEngine;
using UnityEngine.UI;

public class CustomCharacterController : MonoBehaviour {
    public static CustomCharacterController instance;


    public UIAnimatorCore.UIAnimator medicKitUIAnimator;
    [HideInInspector]
    public Image medicKitImage;
    internal Button medicKitButton;
    public AudioSource flagCapturedSound;
    public FPSPlayer fpSPlayer;
    public PlayerWeapons playerWeapons;
    [Header("For Readonly Purpose")]
    public int totalMedicKitsAvailable;
    public Text medicKitText;
    [HideInInspector]
    public int flagCapturedNumbers;

    public GameObject toDestroyPickup,grenadeButton;
    public Button pickupHealthBtn, pickupGrenadeBtn, pickUpWeaponBtn;//mediKitUseBtn;
    Vector3 originalPositionPickupImage;
    public Sprite[] onPickupImages;
    internal WeaponPickup weaponToPickup;
    int totalCoinsPickedUp;
    public int totalEnemiesKilled;
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

  
    private void Awake()
    {
        SingleTon();
    }


    private void Start()
    {
        medicKitImage = medicKitUIAnimator.GetComponent<Image>();
        medicKitButton = medicKitUIAnimator.GetComponent<Button>();
        totalMedicKitsAvailable = PlayerPrefs.GetInt("TotalMedicKits");
        medicKitText.text = totalMedicKitsAvailable.ToString();
        CrntGun = PlayerPrefs.GetInt("Selected_Gun");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickuphealthkit")
        {
            other.GetComponent<HealthPickup>().PickUpItem();
            if (GVSoundManager.Instance)
                GVSoundManager.Instance.PlaySound("pickup_health");
            //if(GameStat.instance.Pickupsound)
            //GameStat.instance.typerSound.PlayOneShot(GameStat.instance.Pickupsound);

            print("Medkit Worked");
        }
        if (other.gameObject.tag == "pickupcoins")
        {
            other.GetComponent<PickupCoins>().PickUpItem();
            if (GVSoundManager.Instance)
                GVSoundManager.Instance.PlaySound("terminal_hacked");
            print("Coins Worked");
        }
        if (other.gameObject.tag == "pickupbullets")
        {
            other.GetComponent<BulletsAllpickup>().PickUpItem();
            if (GVSoundManager.Instance)
                GVSoundManager.Instance.PlaySound("terminal_hacked");
            print("Bullets Worked");
        }
        if (other.gameObject.tag.Equals("WeaponGift"))
        {
            if (GameStat.instance)
            {
                GameStat.instance.weaponGiftPanel.SetActive(true);
            }
        }
        if (other.gameObject.tag == "FinalDestination")
        {
            other.gameObject.tag = "Untagged";
            print("Triggered");
            GameController.instance.SetLevelComplete();
        }
        else if (other.CompareTag("Flag"))
        {
            if (GameStat.instance)
            {
                GameStat.instance.flagImage.gameObject.SetActive(true);
            }
        }
    }




    //public void CoinPickup()
    //{
    //    //GVSoundManager.Instance?.PlayButtonSound(SoundType.Pickup);
    //    originalPositionPickupImage = GameStat.instance.onPickupShow.transform.position;
    //    GameStat.instance.onPickupShow.SetActive(true);
    //    GameStat.instance.onPickupShow.GetComponent<Image>().sprite = onPickupImages[0];

    //    StartCoroutine("PickupImageWait");

    //    totalCoinsPickedUp++;
    //}
    bool isPickUpGun;
    public void GunPickup()
    {
        //GVSoundManager.Instance?.PlayButtonSound(SoundType.Pickup);
        //isPickUpGun = true;

        originalPositionPickupImage = GameStat.instance.onPickupShow.transform.position;
        //GameStat.instance.onPickupShow.SetActive(true);
        //GameStat.instance.onPickupShow.GetComponent<Image>().sprite = onPickupImages[1];

        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlaySound("reward");


        StartCoroutine("PickupImageWait");

        if (weaponToPickup)
        {
            weaponToPickup.GunPickup();
            weaponToPickup = null;
        }

        //fpSPlayer.PickupGrenade();
        if (toDestroyPickup != null)
        {
            Destroy(toDestroyPickup);
            toDestroyPickup = null;
        }
    }

    public void HealthPickup()
    {
        //GVSoundManager.Instance?.PlayButtonSound(SoundType.Pickup);
        originalPositionPickupImage = GameStat.instance.onPickupShow.transform.position;
        GameStat.instance.onPickupShow.SetActive(true);
        GameStat.instance.onPickupShow.GetComponent<Image>().sprite = onPickupImages[2];

        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlaySound("reward");

        StartCoroutine("PickupImageWait");
        totalMedicKitsAvailable++;// = PlayerPrefs.GetInt("TotalMedicKits");
        PlayerPrefs.SetInt("TotalMedicKits", totalMedicKitsAvailable);
        medicKitText.text = totalMedicKitsAvailable.ToString();
       // fpSPlayer.HealPlayer(200f);

        if (toDestroyPickup != null)
        {
            Destroy(toDestroyPickup);
            toDestroyPickup = null;
        }
    }

    public void GrenadePickup()
    {
        //GVSoundManager.Instance?.PlayButtonSound(SoundType.Pickup);
        originalPositionPickupImage = GameStat.instance.onPickupShow.transform.position;
        GameStat.instance.onPickupShow.SetActive(true);
        GameStat.instance.onPickupShow.GetComponent<Image>().sprite = onPickupImages[1];

        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlaySound("reward");

        StartCoroutine("PickupImageWait");

        fpSPlayer.PickupGrenade();
        if (toDestroyPickup != null)
        {
            Destroy(toDestroyPickup);
            toDestroyPickup = null;
        }
    }

    public void CoinPickup()
    {
        //GVSoundManager.Instance?.PlayButtonSound(SoundType.Pickup);
        originalPositionPickupImage = GameStat.instance.onPickupShow.transform.position;
        GameStat.instance.onPickupShow.SetActive(true);
        GameStat.instance.onPickupShow.GetComponent<Image>().sprite = onPickupImages[0];
        if (GVSoundManager.Instance)
            GVSoundManager.Instance.PlaySound("reward");
        StartCoroutine("PickupImageWait");

        totalCoinsPickedUp++;
    }


    IEnumerator PickupImageWait()
    {
        float time = 0f;
        float colorFade = 1f;
        Vector3 lerpTo = originalPositionPickupImage;
        lerpTo.y += 200f;
        while (time < 1)
        {
            GameStat.instance.onPickupShow.transform.position = Vector3.Lerp(GameStat.instance.onPickupShow.transform.position,
            lerpTo, time / 4);
            GameStat.instance.onPickupShow.GetComponent<Image>().color = new Color(255, 255, 255, (colorFade * 2));
            GameStat.instance.onPickupShow.GetComponentInChildren<Text>().color = new Color(255, 255, 255, (colorFade * 2));

            time += Time.deltaTime;
            colorFade -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.2f);

        GameStat.instance.onPickupShow.SetActive(false);
        GameStat.instance.onPickupShow.transform.position = originalPositionPickupImage;
    }



    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            if(GameStat.instance) //junaid change in this function GameStat.instance 
            {
                //                Debug.Log("Flag Detected");
               GameStat.instance.flagImage.fillAmount += GameStat.instance.flagFillImageSpeed * Time.deltaTime;
               GameStat.instance.flagImage.color = Color.Lerp(Color.red, Color.green, GameStat.instance.flagImage.fillAmount);
                if(GameStat.instance.flagImage.fillAmount >= 1)
                {
                    GameStat.instance.flagImage.gameObject.SetActive(false);
                    if (PlayerPrefs.GetString("InfiniteMode") == "No")
                    {
                       EnemyCounter.instance.FlagCaptured();
                    }
                    else if (PlayerPrefs.GetString("InfiniteMode") == "Yes")
                    {
                        flagCapturedNumbers++;

                        //MyNPCWaveManager.instance.StartNextWave();
                    }
                    flagCapturedSound.Play();
                    other.GetComponent<bl_MiniMapItem>().HideItem();
                    other.GetComponent<bl_MiniMapItem>().HideCircleArea();
                    other.gameObject.SetActive(false);
                    GameStat.instance.flagImage.fillAmount = 0;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flag"))
        {
            if (GameStat.instance)
            {
                Debug.Log("Away From Flag");
                GameStat.instance.flagImage.fillAmount = 0;
                GameStat.instance.flagImage.color = Color.red;
                GameStat.instance.flagImage.gameObject.SetActive(false);
            }
        }
        if (other.gameObject.tag.Equals("WeaponGift"))
        {
            if (GameStat.instance)
            {
                GameStat.instance.weaponGiftPanel.SetActive(false);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            if (collision.gameObject.GetComponent<Animator>())
            {
                collision.gameObject.GetComponent<Animator>().enabled = true;
            }
            collision.gameObject.tag = "Untagged";
        }
    }


    public void RefillPlayerHealth()
    {
       // Debug.Log("Medic Button Clicked");
        if (totalMedicKitsAvailable > 0)
        {

           
            if (fpSPlayer)
                fpSPlayer.GetComponent<InputControl>().SetValueInjectHold(true);



            if (medicKitButton)
                medicKitButton.interactable = false;
            if (grenadeButton && grenadeButton.GetComponent<TouchButton>())
            {
                grenadeButton.SetActive(false);

            }
            StartCoroutine(FillPlayerHealth());
        }
    }


    IEnumerator FillPlayerHealth()
    {
        yield return new WaitForSeconds(2f);
        fpSPlayer.hitPoints = 100;
        fpSPlayer.maximumHitPoints = 100;
        fpSPlayer.newHealthBarFillImage.fillAmount = 1;
        totalMedicKitsAvailable -= 1;
        PlayerPrefs.SetInt("TotalMedicKits", totalMedicKitsAvailable);
        medicKitText.text = totalMedicKitsAvailable.ToString();
        if (totalMedicKitsAvailable <= 0)
        {
            PlayerPrefs.SetInt("TotalMedicKits", 0);

        }

        yield return new WaitForSeconds(1f);

        if (medicKitButton)
            medicKitButton.interactable = true;
        if (grenadeButton)
            grenadeButton.SetActive(true);
        StopCoroutine(FillPlayerHealth());
    }


    int CrntGun ;
    
    public void SwitchUnlockedWeapons(int gunIndex)
    {
        if (GameStat.instance.zoomImage.activeInHierarchy)
            GameStat.instance.zoomImage.SetActive(false);
        if (PlayerPrefs.GetInt(GenericPopup.Instance.weaponsData.weaponsList[gunIndex - 1].weaponName) == 1)
            CrntGun = gunIndex;
        GameStat.instance.WeaponSelectionPanel();
        playerWeapons.SelectWeaponBySam(CrntGun);
        if (GVSoundManager.Instance && GVSoundManager.Instance.isSoundON())
            GVSoundManager.Instance.PlayBtnClickSound();
        //fpSPlayer.rectTransform.gameObject.SetActive(true);
    }
}
