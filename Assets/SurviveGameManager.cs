using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;

namespace Academy.HoloToolkit.Unity
{
    public class SurviveGameManager : Singleton<HandsManager>
    {
        GameObject player;
        public bool HandDetected
        {
            get;
            private set;
        }
        // Use this for initialization
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag(Strings.Player);

            InteractionManager.InteractionSourceReleased += InteractionManager_InteractionSourceReleased;
            InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;
            InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;
            InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;

            

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
        {

        }

        private void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs obj)
        {

        }


        private void InteractionManager_InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
        {

            Debug.Log("press survive game manager");

            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            RaycastHit hit;
            if (Physics.Raycast(headPosition, gazeDirection, out hit))
                {
                    if (hit.collider.gameObject.name == "Yes_button")
                    {
                        SceneManager.LoadSceneAsync("loading2", LoadSceneMode.Additive);

                    }
                    else if (hit.collider.gameObject.name == "No_button")
                    {
                        SceneManager.LoadScene("main");


                    }
                }
            if(player.active == true)
            {
                player.GetComponent<PlayerFireBullet>().fire();
            }
        }
        private void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
        {

        }

        void OnDestroy()
        {
            InteractionManager.InteractionSourceDetected -= InteractionManager_InteractionSourceDetected;
            InteractionManager.InteractionSourceLost -= InteractionManager_InteractionSourceLost;
            InteractionManager.InteractionSourcePressed -= InteractionManager_InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased -= InteractionManager_InteractionSourceReleased;
        }
    }
}
