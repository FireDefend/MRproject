using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {


	bool isStopped=false;
	GameObject parse;
	void Awake(){
		parse = GameObject.FindGameObjectWithTag ("Parse");
	}

	void Start(){
		parse.SetActive (false);

	}
	void Update(){

		Parse ();

		if (EventController.Instance.isPlayerLose) {

			if (Input.GetKeyDown(KeyCode.Space)) {
				SceneManager.LoadScene ("Level1");

			}

		}




	}

	void Parse(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (isStopped==false) {
				Time.timeScale = 0;
				isStopped = true;
				parse.SetActive (true);
			}
			else if (isStopped==true) {
				Time.timeScale = 1;
				isStopped = false;
				parse.SetActive (false);
			}


		}


	}







}
