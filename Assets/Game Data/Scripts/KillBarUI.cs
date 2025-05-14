using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillBarUI : MonoBehaviour
{
    public Text killerText;
    public Text killedText;

    public void DisplayUI(string killerName, string killedName)
    {
        killerText.text = killerName;
        killerText.color = Color.green;
        killedText.text = killedName;
        killedText.color = Color.red;
        Invoke("DestroyKillBar", 2f);
    }
    void DestroyKillBar()
    {
        Destroy(gameObject);
    }
}
