using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;

namespace Academy.HoloToolkit.Unity
{
    public class SurviveGameManager : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
        {

            Debug.Log("press survive game manager");

            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(headPosition, gazeDirection, out hit))
                {
                    if (hit.collider.gameObject.name == "Yes_button")
                    {
                        SceneManager.LoadSceneAsync("loading2", LoadSceneMode.Single);

                    }
                    else if (hit.collider.gameObject.name == "No_button")
                    {
                        SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);

                    }
                }
        }
    }
}
