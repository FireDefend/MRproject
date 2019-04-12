using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniteController : MonoBehaviour {


	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;
	Transform pos1;
	Transform pos2;
	Transform pos3;
	void Awake(){
		pos1 = transform.GetChild (0);
		pos2 = transform.GetChild (1);
		pos3 = transform.GetChild (2);

	}
    void Start() {
        //InvokeRepeating("Inite1", 2, 5);
        //InvokeRepeating("Inite2", 3, 8);
        //InvokeRepeating("Inite3", 5, 12);
        Inite1();
        Inite2();
        Inite3();
    }


	void Inite1(){
		Instantiate (enemy1,pos1.position,Quaternion.identity);

	}

	void Inite2(){
		Instantiate (enemy2,pos2.position,Quaternion.identity);
	}

	void Inite3(){
		Instantiate (enemy3,pos3.position,Quaternion.identity);
	}

}
