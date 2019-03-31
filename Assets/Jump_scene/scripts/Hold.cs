using UnityEngine.XR.WSA.Input;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// HandsManager keeps track of when a hand is detected.
/// </summary>
namespace Academy.HoloToolkit.Unity
{
    public class Hold : Singleton<HandsManager>
    {
       
        private bool hold_sym = false;
        private float thrust = 0f;
        private bool start_sym = false;
        private Rigidbody rb;
        private Vector3 J_vector;
        Ray ray;
        RaycastHit hit;
        /// <summary>
        /// Tracks the hand detected state.
        /// </summary>
        public bool HandDetected
        {
            get;
            private set;
        }

        // Keeps track of the GameObject that the hand is interacting with.
        public GameObject FocusedGameObject { get; private set; }

        void Awake()
        {


            rb = this.transform.Find("Qmiku").GetComponent<Rigidbody>();
            this.transform.Find("Canvas").gameObject.SetActive(false);
            this.transform.Find("Cursor").gameObject.SetActive(false);
            InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased += InteractionManager_InteractionSourceReleased;
           

            FocusedGameObject = null;
        }

        private void InteractionManager_InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
        {
            Debug.Log("release");
            hold_sym = false;
        }

        private void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
        {

            Debug.Log("press");

            if (Qmiku.Canvas_sym==true)
            {
                var headPosition = Camera.main.transform.position;
                var gazeDirection = Camera.main.transform.forward;

                if (Physics.Raycast(headPosition, gazeDirection, out hit))
                {
                    if (hit.collider.gameObject.name == "Yes_button")
                    {
                        this.transform.Find("Qmiku").transform.position = Cubemanager.orginal_miku;
                        this.transform.Find("Qmiku").GetComponent<Rigidbody>().drag = 20;
                        this.transform.Find("Cubemanager").transform.Find("Cube(Clone)").transform.position = Cubemanager.orginal_cube;
                        this.transform.Find("Cubemanager").transform.Find("Cube(Clone)").GetComponent<Rigidbody>().drag = 20;
                        this.transform.Find("Canvas").gameObject.SetActive(false);
                        this.transform.Find("Cursor").gameObject.SetActive(false);
                        Qmiku.Canvas_sym = false;
                        Qmiku.grade = 0;
                    }
                }
            }
            else
            {
                hold_sym = true;
            }
        }


        void OnDestroy()
        {

            InteractionManager.InteractionSourcePressed -= InteractionManager_InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased -= InteractionManager_InteractionSourceReleased;
        }
        void Update()
        {
            if (hold_sym == false && start_sym == true)
            {
                float length = Mathf.Sqrt(Mathf.Pow(Qmiku.miku_dir.x, 2) + Mathf.Pow(Qmiku.miku_dir.z, 2));
                J_vector.x = Qmiku.miku_dir.x / length;
                J_vector.z = Qmiku.miku_dir.z / length;
                J_vector.y = 1f;
                rb.AddForce(J_vector * thrust);
                thrust = 0f;
                start_sym = false;
            }
            if (hold_sym == true)
            {
                thrust += 2f;
                start_sym = true;
            }

        }
    }
}