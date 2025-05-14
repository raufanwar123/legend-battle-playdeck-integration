using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GVAllPluginsDonotDestroy : MonoBehaviour {

    public static GVAllPluginsDonotDestroy _instance = new GVAllPluginsDonotDestroy();
    
    public static GVAllPluginsDonotDestroy instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GVAllPluginsDonotDestroy>();
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
    // Use this for initialization
    void Start () {
		DontDestroyOnLoad (gameObject);
	}

}
