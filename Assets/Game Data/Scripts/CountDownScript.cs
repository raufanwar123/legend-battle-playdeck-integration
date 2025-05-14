using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownScript : MonoBehaviour
{
    public Image loadingImageComponent;
    public Text TextforTips;
    public int timeLeft; 
    public Text text;
    public AudioClip[] countDownClips;
    public AudioSource audioSource;
    public string[] AllTips = { "Tips for Sniper: Press the aim button to aim and press fire button to shoot", "Tips: Bonus Contains many free weapons, Don't forget to claim!", "Tips: Options contains many usefull settings, please take a look!", "Tips: Purchase any product can remove ads!" };

    public GameObject VersusScreen;

    private void OnEnable()
    {
        timeLeft = 5/*.0f*/;
        text.text = "" + timeLeft;

    }

    void Start()
    {
        //timeLeft = Random.Range(5, 10);
        StartCoroutine(Countdown());
        Time.timeScale = 0;
    }

    void Countdown1()
    {
        timeLeft--;
        text.text = "Time Left: " + timeLeft;
        Debug.Log("countdown time is " + timeLeft);
        while (timeLeft > 0)
        {
            Debug.Log(timeLeft);
            Start();
        }

        if (timeLeft <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            audioSource.clip = countDownClips[timeLeft-1];
            audioSource.Play();
            text.text = "" + timeLeft;
            timeLeft--;
            yield return new WaitForSecondsRealtime (1f);
        }
        this.gameObject.SetActive(false);
        VersusScreen.SetActive(true);
    }


    private void OnDisable()
    {
        timeLeft = 5/*.0f*/;
        text.text = "" + timeLeft;

    }
}







