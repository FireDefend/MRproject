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
            hold_sym = true;
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
                float length = Mathf.Sqrt(Mathf.Pow(Cube.miku_dir.x, 2) + Mathf.Pow(Cube.miku_dir.z, 2));
                J_vector.x = Cube.miku_dir.x / length;
                J_vector.z = Cube.miku_dir.z / length;
                J_vector.y = 1f;
                rb.AddForce(J_vector * thrust);
                thrust = 0f;
                start_sym = false;
            }
            if (hold_sym == true)
            {
                thrust += 5f;
                start_sym = true;
            }

        }
    }
}