using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotationScript : MonoBehaviour {

    public int speed;

	// Update is called once per frame
	void FixedUpdate () {
        // ...also rotate around the World's Y axis
        transform.Rotate(Vector3.up * Time.fixedUnscaledDeltaTime * speed, Space.World);
    }
}