using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GVLogsManager : MonoBehaviour
{
    
    public static GVLogsManager _instance = new GVLogsManager();

    public static GVLogsManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GVLogsManager>();
                if (_instance && _instance.gameObject)
                {
                    
                    if (Application.isPlaying)
                    {
                        // The game is running
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                    else
                    {
                        // The script is executing inside the editor
                    }
                    

                }
            }
            return _instance;
        }
    }

    public void DebugLog(Object thisClass, string logstring)
    {
        //if (GVAdsManager.Instance._GVLogs == true)
            Debug.Log("GVLogsManager: " + thisClass.GetType().Name + ": " + logstring);
    }

    public void DebugLogError(object thisClass, string logsstring)
    {
        //if (GVAdsManager.Instance._GVLogs ==  true)
            Debug.LogError("GVLogsManager: " + thisClass.GetType().Name + ": " + logsstring);
    }
}
