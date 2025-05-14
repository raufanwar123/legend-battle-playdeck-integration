using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    public Image _SplashImg;
    public GameObject _LoadingScreen;


    string deviceInfo = "DEVICE INFO: ";
    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;
        GameConfiguration.SetFirstTimePlay();
 
        StartCoroutine(ShowLoadingScreen());


    } 


    IEnumerator ShowLoadingScreen()

    {
        yield return new WaitForSecondsRealtime(0.5f);
        
        yield return new WaitForSecondsRealtime(1f);
        ShowLoading();

    }
    void ShowLoading()
    {
        _SplashImg.gameObject.SetActive(false);
        
        _LoadingScreen.SetActive(true);
        StartCoroutine(_LoadingScreen.GetComponent<Loading>().LoadScene(2));
    }

    void LoadMenu()
    {
        SceneManager.LoadScene(2);
    }

    void LogDeviceInfo()
    {
        
        Debug.Log(deviceInfo + "deviceModel: " + SystemInfo.deviceModel);
        Debug.Log(deviceInfo + "deviceName: " + SystemInfo.deviceName);
        Debug.Log(deviceInfo + "deviceType: " + SystemInfo.deviceType);
        Debug.Log(deviceInfo + "graphicsDeviceID: " + SystemInfo.graphicsDeviceID);
        Debug.Log(deviceInfo + "graphicsDeviceName: " + SystemInfo.graphicsDeviceName);
        Debug.Log(deviceInfo + "graphicsDeviceType: " + SystemInfo.graphicsDeviceType);
        Debug.Log(deviceInfo + "graphicsDeviceVendor: " + SystemInfo.graphicsDeviceVendor);
        Debug.Log(deviceInfo + "graphicsDeviceVersion: " + SystemInfo.graphicsDeviceVersion);
        Debug.Log(deviceInfo + "graphicsMemorySize: " + SystemInfo.graphicsMemorySize);
        Debug.Log(deviceInfo + "maxTextureSize: " + SystemInfo.maxTextureSize);
        Debug.Log(deviceInfo + "operatingSystem: " + SystemInfo.operatingSystem);
        Debug.Log(deviceInfo + "processorCount: " + SystemInfo.processorCount);
        Debug.Log(deviceInfo + "processorFrequency: " + SystemInfo.processorFrequency);
        Debug.Log(deviceInfo + "processorType: " + SystemInfo.processorType);
        Debug.Log(deviceInfo + "systemMemorySize: " + SystemInfo.systemMemorySize);

    }

    void SetQualitySettings()
    {
        int RAMMBs = SystemInfo.systemMemorySize;
        if (RAMMBs >= 3000) //Greater than 3GB
        {
            QualitySettings.SetQualityLevel(2); //Very High Quality
            Debug.Log(deviceInfo +  "QUALITY SETTING: Very High");
        }
        else if (RAMMBs < 3000 && RAMMBs > 1000)//From 1 GB to 2GB
        {
            QualitySettings.SetQualityLevel(1); //Medium Quality
            Debug.Log(deviceInfo + "QUALITY SETTING: MEDIUM");
        }
        else //Less than equal to 1 GB
        {
            QualitySettings.SetQualityLevel(0); //Very Low Quality
                Debug.Log(deviceInfo + "QUALITY SETTING: Very LOW");
        }
    }
}
