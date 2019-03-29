using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour {
    private float speed=0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(Vector3.down*speed * Time.deltaTime, Space.World);
    }
}
