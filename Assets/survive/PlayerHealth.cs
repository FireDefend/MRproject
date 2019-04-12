using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour {


	public float fullHealth=100;
	float curHealth;
	Animation ani;
	Image bloodFillImage;
	Text gameOverText;
	Image getHurtImage;
	void Awake(){
		curHealth = fullHealth;
		ani = GetComponent<Animation> ();
		bloodFillImage = GameObject.FindGameObjectWithTag ("BloodFillImage").GetComponent<Image>();
		gameOverText = GameObject.FindGameObjectWithTag ("GameOverText").GetComponent<Text>();
		getHurtImage = GameObject.FindGameObjectWithTag ("GetHurtImage").GetComponent<Image>();
	}

	void Start(){
		gameOverText.gameObject.SetActive (false);
	}


	public void MyPlayerGetHurt(float damage){

		curHealth -= damage;
		StopAllCoroutines ();
		StartCoroutine (BloodRed());
		bloodFillImage.fillAmount = curHealth / fullHealth;
		if (curHealth<=0) {
			Death ();
		}

	}

	IEnumerator BloodRed(){
		
		for (float i = 1; i >= 0; i-=0.1f) {
			getHurtImage.color = new Color (1,0,0,i);
			if (i<=0.11f) {
				getHurtImage.color = new Color (1,0,0,0);
				break;
			}
			yield return new WaitForSeconds (0.02f);

		}


	}

	void Death(){
		EventController.Instance.isPlayerLose = true;
		StopAllCoroutines ();
		getHurtImage.enabled = false;
		gameOverText.gameObject.SetActive (true);
		ani.clip = GetComponent<PlayerController> ().deathClip;
		ani.Play ();
		gameObject.SetActive (false);
	}



}
