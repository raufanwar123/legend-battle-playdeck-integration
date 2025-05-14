using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

    public GameObject[] Levels;

    public Button backBtn;
    public Button selectBtn;
    public Button unlockLevelBtn;

    public Sprite LockedImage;
    public Sprite UnlockedImage;

    public Text CashTxt;

    int currLevelsCompleted = 0;

    private void Awake()
    {
        instance = this;

        backBtn.onClick.AddListener(OnBackBtnCLick);
        selectBtn.onClick.AddListener(OnSelectBtnCLick);
        unlockLevelBtn.onClick.AddListener(UnlockNextLevel);
    }

    private void Start()
    {     
        Initialize();
    }

    private void Initialize()
    {
        currLevelsCompleted = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CompletedLevelsKey);
        print("thse are completed levels sor far : " + currLevelsCompleted);
        GameConfiguration.SetIntegerKeyValue(GameConfiguration.SelectedLevelKey, currLevelsCompleted);
        CashTxt.text = GameConfiguration.GetIntegerKeyValue(GameConfiguration.CashKey).ToString();
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].GetComponent<Button>().gameObject.GetComponent<Image>().sprite = LockedImage;
            //Levels[i].GetComponent<Button>().interactable = false;
            Levels[i].GetComponent<Button>().gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        UnlockCompletedLevels();
    }
    private void UnlockCompletedLevels()
    {      
        if (currLevelsCompleted == 0)
        {
            currLevelsCompleted = 1;
        }

        for (int i = 0; i < Levels.Length; i++)
        {
            if (i < currLevelsCompleted)
            {
                Levels[i].GetComponent<Button>().gameObject.GetComponent<Image>().sprite = UnlockedImage;
                Levels[i].GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
                //Levels[i].GetComponent<Button>().interactable = true;
                print(i);
            }
            else
            {
                Levels[i].GetComponent<Button>().gameObject.GetComponent<Image>().sprite = LockedImage;
                Levels[i].GetComponent<Button>().transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
                //Levels[i].GetComponent<Button>().interactable = false;
            }
            Levels[i].GetComponent<Button>().gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        Levels[currLevelsCompleted -1].GetComponent<Button>().gameObject.transform.GetChild(1).gameObject.SetActive(true);
        GameConfiguration.SetIntegerKeyValue(GameConfiguration.SelectedLevelKey, currLevelsCompleted);
    }

    public void OnBackBtnCLick()
    {
        if (MainMenuController.instance)
        {
            MainMenuController.instance.OnButtonClickSound(true);
            MainMenuController.instance.LevelSelectionPanel.SetActive(false);
        }
    }

    public void LevelClicked(int val)
    {
        if (val <= currLevelsCompleted)
        {
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.SelectedLevelKey, val);
            unSelectAll(val);
        }

        if (MainMenuController.instance)
        {
            MainMenuController.instance.OnButtonClickSound();
        }
    }


    private void unSelectAll(int val)
    {
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i].GetComponent<Button>().gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        Levels[val - 1].GetComponent<Button>().gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void OnSelectBtnCLick()
    {
        if (MainMenuController.instance)
        {
            MainMenuController.instance.OnButtonClickSound();
            MainMenuController.instance.ShowItemSelectionPanel(true);
        }
    }


    public void UnlockNextLevel()
    {
        if (MainMenuController.instance)
        {
            MainMenuController.instance.OnButtonClickSound();
#if UNITY_EDITOR
            print("HAHAHHAAHAH");
            GameConfiguration.SetIntegerKeyValue(GameConfiguration.CompletedLevelsKey, currLevelsCompleted + 1);
            Initialize();
#endif
            

        }
    }
}
