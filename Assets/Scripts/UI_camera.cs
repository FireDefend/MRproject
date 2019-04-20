using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_camera : MonoBehaviour {
    private float distance;
    private Vector3 front;
    private int count = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count++;
        if (count == 30)
        {
            count = 0;
            this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
            front = Camera.main.transform.position + Camera.main.transform.forward*2;

            distance = Mathf.Sqrt(Mathf.Pow(front.x - transform.position.x, 2) + Mathf.Pow(front.y - transform.position.y, 2) + Mathf.Pow(front.z - transform.position.z, 2));
            if (distance > 1)
            {
                this.transform.position = Camera.main.transform.position + Camera.main.transform.forward*2;
            }
        }
    }
}
