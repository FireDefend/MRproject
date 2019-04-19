using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_weather : MonoBehaviour {
    private Vector3 look_rotation1;
    private Vector3 look_rotation2;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        this.transform.position = this.transform.parent.transform.position - this.transform.parent.transform.right*0.25f+this.transform.up*0.3f;
        look_rotation1 = -Camera.main.transform.forward;
        look_rotation2 = new Vector3(look_rotation1.x, 0, look_rotation1.z);
        this.transform.parent.rotation= Quaternion.LookRotation(look_rotation2);
    }
}
