using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireBullet : MonoBehaviour {


	public int thisWeaponIndex=1;
	GameObject firstBullet;
	GameObject secondBullet;
	GameObject thirdBullet;
	Transform firePos;
	public float fireSpeed=15;
	void Awake(){
		firstBullet = Resources.Load<GameObject> ("PlayerBullet/FirstBullet");
		secondBullet = Resources.Load<GameObject> ("PlayerBullet/SecondBullet");
		thirdBullet = Resources.Load<GameObject> ("PlayerBullet/ThirdBullet");
		firePos = transform.GetChild (3).transform;
	}

	void Update(){
		if (Input.GetMouseButtonDown(0)) {
			switch(thisWeaponIndex){
			case 1:
				FireFirstBullet ();
				break;
			case 2:
				FireSecondBullet ();
				break;
			case 3:
				FireThirdBullet ();
				break;

			}
		}

	}


	void FireFirstBullet(){

		GameObject g = Instantiate (firstBullet,firePos.position,firePos.rotation);

		g.GetComponent<Rigidbody>().AddForce(g.transform.forward*fireSpeed);
	}

	void FireSecondBullet(){
		GameObject g = Instantiate (secondBullet,firePos.position,firePos.rotation);
		g.GetComponent<Rigidbody>().AddForce(g.transform.forward*fireSpeed);


	}

	void FireThirdBullet(){

		GameObject g = Instantiate (thirdBullet,firePos.position,firePos.rotation);
		g.GetComponent<Rigidbody>().AddForce(g.transform.forward*fireSpeed);


	}


}
