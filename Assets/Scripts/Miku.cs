using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miku : MonoBehaviour
{
    private Animator ani;
   // private AudioSource aud;
    public float speed = 10.0f;
 
    Ray ray;
    RaycastHit hit;
    bool signal = false;
    Vector3 route;
    Vector3 hitpoint;

    public static Miku Instance { get; private set; }



    // Use this for initialization
    void Start()
    {
        Instance = this;
        ani = GetComponent<Animator>();
       // aud = GetComponent<AudioSource>();
        ani.SetInteger("walk", 1);


    }

    // Update is called once per frame
    void Update()
    {
        if (signal == true)
        {

            
            transform.rotation = Quaternion.LookRotation(route);
            transform.Translate(route *0.5f * Time.deltaTime,Space.World);
            if ((hitpoint-transform.position).magnitude<0.1)
            {
                signal = false;
                ani.SetInteger("walk", 1);
            }
        }
      
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                hitpoint = hit.point;
                ani.SetInteger("walk", 2);
                float one = Mathf.Sqrt(Mathf.Pow(hit.point.x - transform.position.x,2) + Mathf.Pow(hit.point.z - transform.position.z,2));
                route = new Vector3((hit.point.x - transform.position.x)/one, 0, (hit.point.z-transform.position.z)/one);

                signal = true;


            }
            
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            ani.SetInteger("walk", 3);

       

        }
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("idle_face_vmd 0"))
        {
            ani.SetInteger("walk", 1);
        }


    }
}
