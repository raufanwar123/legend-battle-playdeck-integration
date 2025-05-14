using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinAnimate : MonoBehaviour
{

    public Text[] texts;
    public Text totalCashText;
    public bool last,playsound = false;

    //public GameObject coinimg, secndcoin;


    //IEnumerator coinstop()
    //{
    //    yield return new WaitForSeconds(6f);
    //    coinimg.SetActive(false);
    //    secndcoin.SetActive(true);
    //}
    private void OnEnable()
    {
        string strin;
        for (int i = 0; i < texts.Length; i++)
        {
     //       Time.timeScale = 1;
            strin = texts[i].text;

            //    no = int.Parse(strin);

            // print(no);
            if (i == texts.Length - 1)
            {
                last = true;
                playsound = true;
            }
            else
               last = false;  
            //StartCoroutine(ForAllBox(texts[i], int.Parse(strin),last));
        }
        StartCoroutine(ForAllBox(totalCashText, int.Parse(totalCashText.text), true));
       // StartCoroutine(coinstop());
    }

    IEnumerator ForAllBox(Text text, int cash,bool lst)
    {
        int speed = 10;
        yield return null;
      //  yield return new WaitForSeconds(1.5f);
        if (cash < 1000)
        {
            speed = cash/30;
        }
        else if (cash < 3000)
        {
            speed = cash / 50;
        }
        else if (cash > 3000 && cash< 5000)
        {
            speed = cash/100;
        }
        else
            speed = (cash/100)+300;

        StartCoroutine(Reward_Animation(cash, text,speed, lst));
    }

    IEnumerator Reward_Animation(int amount, Text text, int amountAddInTemp, bool last, float time = 0.01f, float delayTime = 0.8f)
    {
        int temp = 0;
        text.text = "";
        yield return new WaitForSecondsRealtime(delayTime);
        while (temp <= amount)
        {
            temp += amountAddInTemp;
           // Debug.Log("Total Loop");
            text.text = temp.ToString();
            if (last)
            {
                if (GVSoundManager.Instance)
                    GVSoundManager.Instance.PlaySound("typewriter-key");
                //GameStat.instance.typerSound.PlayOneShot(GameStat.instance.typerClip);
                //last = true;
                //playsound = true;
            }
            yield return new WaitForSecondsRealtime(time);
        }
        text.text = amount.ToString();
    }
}
