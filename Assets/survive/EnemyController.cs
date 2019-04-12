using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {


		
	NavMeshAgent nav;
	public float attackDis=0.15f;
	Transform playerTrans;
	[HideInInspector]
	public bool isCanMove=true;
	Animator ani;
	public bool isCanAttack=true;

	void Awake(){

		nav = GetComponent<NavMeshAgent> ();
		playerTrans = GameObject.FindGameObjectWithTag (Strings.Player).transform;
		ani = GetComponent<Animator> ();
	}


	void FixedUpdate(){
		if (isCanMove==false) {
			return;
		}
		if (Vector3.Distance (transform.position, playerTrans.position) < attackDis) {

			Attack ();
		} else {

			Move ();

		}

	}



	void Move(){
		nav.Resume();
		nav.SetDestination (playerTrans.position);

	}


	void Attack(){
		
		nav.Stop ();
		ani.SetTrigger ("Attack");
		if (isCanAttack==false) {
			return;
		}
		isCanAttack = false;
		Invoke ("OpenAttack",1);
		Invoke ("CanAttack",2.5f);
	}

	void OpenAttack(){
		transform.GetChild (2).gameObject.SetActive(true);

	}


	void CanAttack(){
		isCanAttack = true;
	}

}
