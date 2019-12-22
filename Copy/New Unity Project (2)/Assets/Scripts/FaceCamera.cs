using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        var v = transform.position - Camera.main.transform.position;
        v.y = 0.0f;
        transform.rotation = Quaternion.LookRotation(v);
    }
}
