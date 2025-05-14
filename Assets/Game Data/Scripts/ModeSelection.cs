using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelection : MonoBehaviour
{
    public void GoToMenu()
    {
        
        MainMenuController.instance.MainMenuPanel.SetActive(true);
        gameObject.SetActive(false);
        MainMenuController.instance.OnButtonClickSound();
    }

    public void ShowLevelSelection()
    {
        PlayerPrefs.SetString("InfiniteMode", "No");
        gameObject.SetActive(false);
        MainMenuController.instance.LevelSelectionPanel.SetActive(true);
    }
    public void ShowGunSelection()
    {
        PlayerPrefs.SetString("InfiniteMode", "Yes");
        gameObject.SetActive(false);
        MainMenuController.instance.ItemSelectionPanel.SetActive(true);
    }
}
