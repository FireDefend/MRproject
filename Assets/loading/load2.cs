using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


/// <summary>
/// </summary>
public class load2 : MonoBehaviour
{
    private Slider slider;         
    float currentProgress; 
    int targetProgress;  
    private bool loadscene_sym;
    private bool oneload_sym;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "main")
        {
            SceneManager.UnloadSceneAsync("main");
        }else if (SceneManager.GetActiveScene().name == "survive")
        {
            SceneManager.UnloadSceneAsync("survive");
        }
        
        loadscene_sym = false;
        oneload_sym = true;
        currentProgress = 0;
        targetProgress = 0;
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = 0;
        //StartCoroutine(LoadingScene()); 
    }


    /// <summary>
    /// </summary>
    /// <returns></returns>
   /* private IEnumerator LoadingScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("jump"); 
        asyncOperation.allowSceneActivation = false;                  
        while (asyncOperation.progress < 0.9f)                                
        {
            targetProgress = (int)(asyncOperation.progress * 100);
            yield return LoadProgress();
        }
        targetProgress = 100; 
        yield return LoadProgress();
       
        asyncOperation.allowSceneActivation = true;
        

    }


    /// <summary>
    /// </summary>
    /// <returns></returns>
    private IEnumerator<WaitForEndOfFrame> LoadProgress()
    {
        while (currentProgress < targetProgress)
        {
            currentProgress+=0.5f;                            
            slider.value = (float)currentProgress / 100; 
            yield return new WaitForEndOfFrame();         
        }

    }*/
    private void Update()
    {
        slider.value+=0.005f;
        if (slider.value == 1)
        {
            loadscene_sym = true;
        }
        if (loadscene_sym == true && oneload_sym == true)
        {
            oneload_sym = false;
            SceneManager.LoadSceneAsync("survive", LoadSceneMode.Additive);


        }

        this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
    }
}