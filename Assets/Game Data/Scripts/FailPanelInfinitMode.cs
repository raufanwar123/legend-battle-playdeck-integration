using UnityEngine;
using UnityEngine.UI;

public class FailPanelInfinitMode : MonoBehaviour
{
    public Text previousEnemyKillsText, currentEnemyKillsText, previousFlagsCapturedText, currentFlagsCapturedText;
    int previousEnemyKills, currentEnemyKills, previousFlagCaptured, currentFlagCaptured;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Once") == 0)
        {
            previousEnemyKills = EnemyCounter.instance.RemainingEnemies;
            currentEnemyKills = EnemyCounter.instance.RemainingEnemies;
            previousFlagCaptured = CustomCharacterController.instance.flagCapturedNumbers;
            currentFlagCaptured = CustomCharacterController.instance.flagCapturedNumbers;

            UpdateText();

            PlayerPrefs.SetInt("PreviousEnemyKills", previousEnemyKills);
            PlayerPrefs.SetInt("PreviousFlagCaptured", previousFlagCaptured);
            PlayerPrefs.SetInt("Once", 1);
            
        }
        else if (PlayerPrefs.GetInt("Once") == 1)
        {
            currentEnemyKills = EnemyCounter.instance.RemainingEnemies;
            currentFlagCaptured = CustomCharacterController.instance.flagCapturedNumbers;

            


            if (currentEnemyKills > previousEnemyKills)
            {
                UpdateText();
                PlayerPrefs.SetInt("PreviousEnemyKills", currentEnemyKills);
            }
            if (currentFlagCaptured > previousFlagCaptured)
            {
                UpdateText();
                PlayerPrefs.SetInt("PreviousFlagCaptured", currentFlagCaptured);
            }
        }
    }

    private void UpdateText()
    {
        previousEnemyKillsText.text = PlayerPrefs.GetInt("PreviousEnemyKills").ToString();
        currentEnemyKillsText.text = currentEnemyKills.ToString();
        previousFlagsCapturedText.text = PlayerPrefs.GetInt("PreviousFlagCaptured").ToString();
        currentFlagsCapturedText.text = currentFlagCaptured.ToString();
    }
}
