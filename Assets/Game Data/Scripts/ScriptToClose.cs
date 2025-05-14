using System.Collections;
using UnityEngine;

public class ScriptToClose : MonoBehaviour
{
    public GameObject panel;
    public float time = 3f;
    public WaitForSeconds wfs;
    public bool offpanel;
    private void Start()
    {
        if (GVSoundManager.Instance && GVSoundManager.Instance.isSoundON())
        {
            if (GetComponent<Animator>())
            {
                GetComponent<Animator>().enabled = true;
            }
            if (GetComponent<AudioSource>())
            {
                GetComponent<AudioSource>().enabled = true;
            }
        }
        wfs = new WaitForSeconds(time);
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return wfs;
        if (offpanel)
        {
            panel.SetActive(false);
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            gameObject.transform.parent.GetChild(1).gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
