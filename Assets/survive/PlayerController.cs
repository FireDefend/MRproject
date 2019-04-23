using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {



	public enum PlayerState{

		Idle,
		Run,
		GetHurt,
		Death


	}

	PlayerState playerState=PlayerState.Idle;
	Animation ani;
	Animation weaponAni;
	AnimationClip idleClip;
	AnimationClip runClip;
	AnimationClip getHurtClip;
	[HideInInspector]
	public AnimationClip deathClip;

	AnimationClip weaponIdleClip;
	AnimationClip weaponRunClip;
	AnimationClip weaponDeathClip;
	public float moveSpeed=80;
	float x,z;
	int layerMask;
	void Awake(){

		ani = GetComponent<Animation> ();
		weaponAni = transform.GetChild (3).GetComponent<Animation>();
		weaponIdleClip = transform.GetChild (3).GetComponent<Animation> ().GetClip ("Standing_Aim_Idle");
		weaponRunClip = transform.GetChild (3).GetComponent<Animation> ().GetClip ("Run_Forward_Aim");
		weaponDeathClip = transform.GetChild (3).GetComponent<Animation> ().GetClip ("Dead_2");
	}

	void Start(){
		idleClip = ani.GetClip ("Standing_Aim_Idle");
		runClip = ani.GetClip ("Run_Forward_Aim");
		deathClip = ani.GetClip ("Dead_2");
		layerMask = LayerMask.GetMask ("Ground");
	}

	void FixedUpdate(){

		Move ();
		Rotate ();




	}


	void Move(){

		x = Input.GetAxis ("Horizontal");
		z = Input.GetAxis ("Vertical");
		if (x != 0 || z != 0) {
			transform.Translate (new Vector3 (x, 0, z) * Time.deltaTime * moveSpeed,Space.World);
			ani.clip = runClip;
			ani.Play ();
			weaponAni.clip = weaponRunClip;
			weaponAni.Play ();
		} else {
			ani.clip = idleClip;
			ani.Play ();
			weaponAni.clip = weaponIdleClip;
			weaponAni.Play ();
		}


        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;


        //Ray ray= Camera.main.ScreenPointToRay (Input.mousePosition);
        var ray = new Ray(headPosition, gazeDirection);
        RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo,120,layerMask)) {

            Vector3 lookPos = hitInfo.point - transform.position;
			transform.rotation = Quaternion.LookRotation (lookPos);
		}


	}



	void Rotate(){





	}





}
