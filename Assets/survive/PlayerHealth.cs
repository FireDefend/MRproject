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
    GameObject canvasEnd;

    SurviveModelController modelController;
    void Awake(){
		curHealth = fullHealth;
		ani = GetComponent<Animation> ();

        canvasEnd = GameObject.FindGameObjectWithTag("CanvasEnd");
        modelController = GetComponent<SurviveModelController>();
        canvasEnd.SetActive(false);
        //bloodFillImage = GameObject.FindGameObjectWithTag ("BloodFillImage").GetComponent<Image>();
        //gameOverText = GameObject.FindGameObjectWithTag ("GameOverText").GetComponent<Text>();
        //getHurtImage = GameObject.FindGameObjectWithTag ("GetHurtImage").GetComponent<Image>();
    }

	void Start(){
		//gameOverText.gameObject.SetActive (false);
	}


	public void MyPlayerGetHurt(float damage){

		curHealth -= damage;
        Debug.LogError("heal  " + curHealth);
		StopAllCoroutines ();
		StartCoroutine (BloodRed());
		//bloodFillImage.fillAmount = curHealth / fullHealth;
		if (curHealth<=0 || modelController.scoreNum > 200 || transform.position.y < -2) {
			Death ();
		}

	}

	IEnumerator BloodRed(){
		
		for (float i = 1; i >= 0; i-=0.1f) {
			//getHurtImage.color = new Color (1,0,0,i);
			if (i<=0.11f) {
				//getHurtImage.color = new Color (1,0,0,0);
				break;
			}
			yield return new WaitForSeconds (0.02f);

		}


	}

	void Death(){
		EventController.Instance.isPlayerLose = true;
		StopAllCoroutines ();
		//getHurtImage.enabled = false;
		//gameOverText.gameObject.SetActive (true);
		//ani.clip = GetComponent<SurviveModelController> ().deathClip;
		//ani.Play ();

		

        canvasEnd.SetActive(true);

        canvasEnd.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;

        var grade_text = "You got " + GetComponent<SurviveModelController>().scoreNum + " points\n" + "      try again?";
        canvasEnd.transform.Find("Text").GetComponent<Text>().text = grade_text;


        gameObject.SetActive(false);
        //Canvas_sym = true;
    }



}
