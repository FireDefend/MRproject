using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour {

	public float attackValue=30;


	void OnCollisionEnter(Collision col){
        if (col.transform.tag=="Enemy") {
			if (col.transform.GetComponent<EnemyHealth>().enemyCurHealth<=0) {
				return;
			}
			col.transform.GetComponent<EnemyHealth>().EnemyGetHurt(attackValue);
			Destroy (gameObject);
		}


	}







}
