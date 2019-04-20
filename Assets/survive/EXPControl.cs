using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Academy.HoloToolkit.Unity;
public class EXPControl : MonoBehaviour {


	Image expFillImage;
	[HideInInspector]
	public float curEXPCount=0;
	float fullEXPCount=100;
	PlayerFireBullet playerFireBullet;
	void Awake(){

		expFillImage = GetComponent<Image> ();
		playerFireBullet = GameObject.FindGameObjectWithTag (Strings.Player).transform.GetChild(3).GetComponent<PlayerFireBullet>();
	}


	public void AddEXP(float exp){

		curEXPCount += exp;
		expFillImage.fillAmount = curEXPCount / fullEXPCount;
		if (curEXPCount>=fullEXPCount) {
			curEXPCount = 0;
			fullEXPCount = fullEXPCount * 2;
			expFillImage.fillAmount = 0;
			if (fullEXPCount==200) {
				playerFireBullet.thisWeaponIndex = 2;

			}else if (fullEXPCount==400) {
				playerFireBullet.thisWeaponIndex = 3;
			}
		}

	}




}
