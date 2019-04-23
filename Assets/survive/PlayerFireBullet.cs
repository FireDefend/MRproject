using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;


namespace Academy.HoloToolkit.Unity
{
    public class PlayerFireBullet : Singleton<HandsManager>
    {


        public int thisWeaponIndex = 1;
        GameObject firstBullet;
        GameObject secondBullet;
        GameObject thirdBullet;
        Transform firePos;
        public float fireSpeed = 15;

        [HideInInspector]
        public bool isFiring = false;
        public bool HandDetected
        {
            get;
            private set;
        }
        void Awake()
        {
            isFiring = false;
            firstBullet = Resources.Load<GameObject>("PlayerBullet/FirstBullet");
            secondBullet = Resources.Load<GameObject>("PlayerBullet/SecondBullet");
            thirdBullet = Resources.Load<GameObject>("PlayerBullet/ThirdBullet");
            firePos = transform.GetChild(3).transform;
            InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;
            InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;
            InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;
            InteractionManager.InteractionSourceReleased += InteractionManager_InteractionSourceReleased;

        }
        private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
        {

        }

        private void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs obj)
        {

        }

        private void InteractionManager_InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
        {
            Debug.Log("release");
            isFiring = true;
            switch (thisWeaponIndex)
            {
                case 1:
                    FireFirstBullet();
                    break;
                case 2:
                    FireSecondBullet();
                    break;
                case 3:
                    FireThirdBullet();
                    break;

            }
            StartCoroutine(firingAni());

        }

        private void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
        {

        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isFiring = true;
                switch (thisWeaponIndex)
                {
                    case 1:
                        FireFirstBullet();
                        break;
                    case 2:
                        FireSecondBullet();
                        break;
                    case 3:
                        FireThirdBullet();
                        break;

                }
                StartCoroutine(firingAni());
            }

        }
        IEnumerator firingAni()
        {
            yield return new WaitForSeconds(1);
            isFiring = false;
        }


        void FireFirstBullet()
        {

            GameObject g = Instantiate(firstBullet, firePos.position, firePos.rotation);

            g.GetComponent<Rigidbody>().AddForce(g.transform.forward * fireSpeed);
        }

        void FireSecondBullet()
        {
            GameObject g = Instantiate(secondBullet, firePos.position, firePos.rotation);
            g.GetComponent<Rigidbody>().AddForce(g.transform.forward * fireSpeed);


        }

        void FireThirdBullet()
        {

            GameObject g = Instantiate(thirdBullet, firePos.position, firePos.rotation);
            g.GetComponent<Rigidbody>().AddForce(g.transform.forward * fireSpeed);


        }


    }
}
