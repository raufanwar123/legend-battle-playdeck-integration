using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSplash : MonoBehaviour
{

    public GameObject canvasObj;
    // Start is called before the first frame update
    IEnumerator Start()
    {

        yield return new WaitForSeconds(7f);
        canvasObj.SetActive(true);
        SceneManager.LoadScene(1);
    }

 
}
