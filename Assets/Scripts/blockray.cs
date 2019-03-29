using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockray : MonoBehaviour {
    CanvasGroup canva;
    // Use this for initialization
    void Start () {
        canva = transform.GetComponent<CanvasGroup>();
        canva.blocksRaycasts = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
