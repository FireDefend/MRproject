using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {


	Vector3 delPos;
	Transform playerTrans;
	void Awake(){
		playerTrans = GameObject.FindGameObjectWithTag (Strings.Player).transform;

	}

	void Start(){

		delPos = transform.position - playerTrans.position;

	}



	void LateUpdate(){

		transform.position = delPos + playerTrans.position;


	}





}
