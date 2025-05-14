using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class versusScreenScript : MonoBehaviour
{
    float timeLeft;  //junaid added below lines
    void Start()
    {
        timeLeft = 4;
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            timeLeft--;
            yield return new WaitForSecondsRealtime(1f);
        }
        this.gameObject.SetActive(false);
    }
}
