using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Loading : MonoBehaviour
{
    public Sprite[] loadingBackGrounds;
    public Image loadingImageComponent;
    public Text TextforTips, loadingPercentageText;
    public Image loadingFiller;




    float timeLeft;  //junaid added below lines

    public Text text;

    public string[] AllTips = {"Tips for Sniper: Press the aim button to aim and press fire button to shoot","Tips: Bonus Contains many free weapons, Don't forget to claim!","Tips: Options contains many usefull settings, please take a look!","Tips: Purchase any product can remove ads!" };

    private void OnEnable()
    {
        timeLeft = 8.0f;

    }

    public IEnumerator LoadScene(string name)
    {
        float tempTime = 0f;
        loadingFiller.fillAmount = 0;
        while(tempTime<=1f)
        {
            loadingFiller.fillAmount = tempTime;
            loadingPercentageText.text = (loadingFiller.fillAmount * 100f).ToString("f1");
            tempTime += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(name);
    }

    public IEnumerator LoadScene(int no)
    {
        float tempTime = 0f;
        loadingFiller.fillAmount = 0;
        while (tempTime <= 1f)
        {
            loadingFiller.fillAmount = tempTime;
            loadingPercentageText.text = (loadingFiller.fillAmount * 100f).ToString("f0")+" %";
            tempTime += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(no);
    }


    void Start()
    {
        timeLeft = 5.0f;

        int num = Random.Range(0, loadingBackGrounds.Length);
        loadingImageComponent.sprite = loadingBackGrounds[num];

        int tip = Random.Range(0, AllTips.Length);
        TextforTips.text = "" + AllTips[tip];
    }

    void Update()
    {
   //     timeLeft -= Time.deltaTime;
   //     text.text = "Time Left:" + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
        //    Application.LoadLevel("gameOver");
        }
    }

    private void OnDisable()
    {
      //  float timeLeft = 5.0f;
    }

    //private void OnEnable()
    //{
    //    TopBarGameplay.Instance.HideTopBar();
    //    TopBarGameplay.Instance.HideExitBtn();
    //}

    public void Cat()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.VForVictory.UltimateAirportandCustomerManagementGame&pli=1");
    } 
    
    public void MonsterTruck()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.vforvictory.OffRoadSpeedSuspensionTruckDrive");
    } 
    
    public void Shooting()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.VforVictory.CounterTerroristFPSCommandoShooting");
    }
}







