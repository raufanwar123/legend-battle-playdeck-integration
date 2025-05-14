using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestryGameObject : MonoBehaviour {

    public float destroyTime = 0;

	// Use this for initialization
	void Start () {

        Destroy(gameObject , destroyTime);
	}
}
