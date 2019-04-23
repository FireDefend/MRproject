using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.SceneManagement;


    public class PlayerFireBullet : MonoBehaviour
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


        }
 

       
        public void fire()
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
            }

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

