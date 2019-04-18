using UnityEngine.XR.WSA.Input;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public GameObject model_cube;
        private GameObject new_cube;
        private bool hand_sym;
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
            InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;
            InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;
            InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased += InteractionManager_InteractionSourceReleased;
           

            FocusedGameObject = null;
        }

        private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
        {
            hand_sym = false;
            if (hold_sym == true)
            {
                thrust = 0f;
                hold_sym = false;
                start_sym = false;
                Qmiku.Qmiku_ani.SetInteger("motion", 1);
                this.transform.Find("Grade_screen").transform.Find("detect_text").gameObject.SetActive(true);
            }

        }

        private void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs obj)
        {
            hand_sym = true;
            this.transform.Find("Grade_screen").transform.Find("detect_text").gameObject.SetActive(false);
        }

        private void InteractionManager_InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
        {
            Debug.Log("release");
            if (hand_sym == true)
            { hold_sym = false; }

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
                        for (int i = this.transform.Find("Cubemanager").childCount - 1; i >= 0; i--)
                        {
                            Destroy(this.transform.Find("Cubemanager").GetChild(i).gameObject);
                        }
                         this.transform.Find("Qmiku").transform.gameObject.SetActive(true);
                         Qmiku.Qmiku_ani.SetInteger("motion", 1);
                         this.transform.Find("Qmiku").transform.position = Cubemanager.orginal_miku;
                         this.transform.Find("Qmiku").GetComponent<Rigidbody>().drag = 20;
                         new_cube = Instantiate(model_cube, Qmiku.place_cube, Quaternion.identity);
                         new_cube.transform.parent = this.transform.Find("Cubemanager").transform;
                         new_cube.transform.position = Cubemanager.orginal_cube;
                         new_cube.GetComponent<Rigidbody>().drag = 20;
                         this.transform.Find("Canvas").gameObject.SetActive(false);
                         this.transform.Find("Cursor").gameObject.SetActive(false);
                         Qmiku.Canvas_sym = false;
                         Qmiku.grade = -20;
                         this.transform.Find("Grade_screen").transform.Find("Text").gameObject.SetActive(false);
                        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);


                    }
                    else if (hit.collider.gameObject.name == "No_button")
                    {
                        SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);

                    }
                }
            }
            else if(hand_sym==true)
            {
                hold_sym = true;
                Qmiku.Qmiku_ani.SetInteger("motion", 0);
            }
        }


        void OnDestroy()
        {
            InteractionManager.InteractionSourceDetected -= InteractionManager_InteractionSourceDetected;
            InteractionManager.InteractionSourceLost -= InteractionManager_InteractionSourceLost;
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
                Qmiku.Qmiku_ani.SetInteger("motion", 1);
            }
            if (hold_sym == true)
            {
               
                thrust += 2f;
                start_sym = true;
            }

        }
    }
}