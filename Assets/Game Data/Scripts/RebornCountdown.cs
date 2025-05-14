using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RebornCountdown : MonoBehaviour
{
    public Text TextforTips;

    float timeLeft;  //junaid added below lines

    public Text text;

  
    private void OnEnable()
    {
        timeLeft = 3.0f;
        text.text = "" + Mathf.Round(timeLeft);
    }

    void Start()
    {
        timeLeft = 3.0f;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "" + Mathf.Round(timeLeft);
     
    }



}
