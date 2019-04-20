using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniteController : MonoBehaviour {


	public GameObject enemy1;
	public GameObject enemy2;
	public GameObject enemy3;

    [HideInInspector]
    public int curEnemyNum = 0;
    int totalEnemyNum = 5;
    Transform pos1;
	Transform pos2;
	Transform pos3;
	void Awake(){
		pos1 = transform.GetChild (0);
		pos2 = transform.GetChild (1);
		pos3 = transform.GetChild (2);

	}
    void Start() {
        InvokeRepeating("Inite1", 2, 10);
        InvokeRepeating("Inite2", 3, 15);
        InvokeRepeating("Inite3", 5, 20);
    }
    


	void Inite1(){
        if(curEnemyNum < totalEnemyNum)
        {
            Instantiate(enemy1, pos1.position, Quaternion.identity);
            curEnemyNum++;
        }
		

	}

	void Inite2(){
        if (curEnemyNum < totalEnemyNum)
        {
            Instantiate(enemy2, pos2.position, Quaternion.identity);
            curEnemyNum++;
        }
	}

	void Inite3(){
        if (curEnemyNum < totalEnemyNum)
        {
            Instantiate(enemy3, pos3.position, Quaternion.identity);
            curEnemyNum++;
        }
        
	}

}
