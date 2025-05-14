using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomAIController : MonoBehaviour
{
    public Image playerIcon;
    public Image playerFlag;
    public Image healthBarFiller;
    public Text nametext;
    [HideInInspector]
    public string playerName;
    public int factionNumber;
    int randomFlag;
    int randomIcon;
    int randomName;
    public GameObject healthCanvas;
    void Start()
    {
        Intailization();
    }
    void Intailization()
    {
        if (GameConfiguration.GetIntegerKeyValue(GameConfiguration.KEY_MODE) == 0)
            healthCanvas.SetActive(false);
        else
            healthCanvas.SetActive(true);

        if (GameStat.instance)
        {
            randomFlag = Random.Range(0, GameStat.instance.flagSprites.Length);
            randomIcon = Random.Range(0, GameStat.instance.iconSprites.Length);
            if (factionNumber == 1)
            {
                randomName = Random.Range(0, GameStat.instance.teamNames.Length);
            }
            else
            {
                randomName = Random.Range(0, GameStat.instance.enemyNames.Length);
            }

            playerIcon.sprite = GameStat.instance.iconSprites[randomIcon];
            playerFlag.sprite = GameStat.instance.flagSprites[randomFlag];
            playerName = GameStat.instance.enemyNames[randomName];
            nametext.text = playerName;
        }
    }
}
