using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {


	public float attackValue = 10;

	void Start(){
		gameObject.SetActive (false);

	}

	public void OnTriggerEnter(Collider col){

		if (col.transform.tag==Strings.Player) {

			col.GetComponent<PlayerHealth> ().MyPlayerGetHurt(attackValue);
			//transform.parent.GetComponent<EnemyController> ().isCanAttack = true;
			gameObject.SetActive (false);
		}


	}








}
