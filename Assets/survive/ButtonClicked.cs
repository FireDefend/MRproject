using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonClicked : MonoBehaviour {


	Button btn;


	void Awake(){
		btn = GetComponent<Button> ();

	}

	void Start(){
		switch(transform.name){
		case "StartButton":
			btn.onClick.RemoveAllListeners ();
			btn.onClick.AddListener (StartButton);

			break;
		case "TuiChuButton":
			btn.onClick.RemoveAllListeners ();
			btn.onClick.AddListener (TuiChuButton);
			break;

		

		}


	}


	void StartButton(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("Level1");

	}

	void TuiChuButton(){
		Time.timeScale = 1;
		Application.Quit ();
	}


}
