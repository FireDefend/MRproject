using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour {


	public float enemyFullHealth=100;
	public int thisScore=10;
	public float thisEXP =5;
	//[HideInInspector]
	public float enemyCurHealth=100;
	GameObject bloodFX;
	Animator ani;
	Text scoreText;
	EXPControl expControl;

	void Awake(){
        enemyCurHealth = enemyFullHealth;
        bloodFX =Resources.Load<GameObject>("BloodFX");
		ani=GetComponent<Animator>();
		scoreText = GameObject.FindGameObjectWithTag ("ScoreText").GetComponent<Text>();
		expControl = GameObject.FindGameObjectWithTag ("WeaponEXPFillImage").GetComponent<EXPControl>();
    }

	void Start(){

		enemyCurHealth = enemyFullHealth;

	}


	public void EnemyGetHurt(float damage){
		if (enemyCurHealth<=0) {
			return;
		}
		enemyCurHealth -= damage;
        Debug.LogError("enemyCurHealth" + enemyCurHealth);
		ani.SetTrigger ("Hit");
		if (enemyCurHealth<=0) {
			Death ();
		}


	}


	void Death(){
		GetComponent<BoxCollider> ().enabled = false;
		//GetComponent<NavMeshAgent> ().enabled=false;
        if(expControl!=null)
		    expControl.AddEXP (thisEXP);
        if(scoreText!= null)
        {
            int scoreCount = int.Parse(scoreText.text);
            scoreCount += thisScore;
            scoreText.text = scoreCount.ToString();
        }
		
		GetComponent<EnemyController> ().isCanMove = false;
		GetComponent<NavMeshAgent> ().Stop ();
		ani.SetTrigger ("Death");
		Destroy (gameObject,5);
	}



}
