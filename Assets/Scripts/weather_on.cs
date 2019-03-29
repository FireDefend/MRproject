using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weather_on : MonoBehaviour {
    private bool sym = false;
    void Start()
    {
        //this.transform.Find("weather_screen").transform.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Update()
    {
        if (GameManager.selectedButton&&GameManager.selectedButton.name == "weather")
        {
            sym = !sym;
            this.transform.Find("weather_screen").transform.gameObject.SetActive(sym);
            GameManager.selectedButton = null;
        }


    }
}
