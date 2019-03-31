using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator ani;
    bool signal = false;
    bool start_move = false;
    private Vector3 route;
    private  Vector3 last_hitpoint;
    private string buttun_name=null;
    private GameObject creatmodel;
    private Vector3 look_rotation1;
    private Vector3 look_rotation2;



    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animator>();
        ani.SetInteger("walk", 1);
        last_hitpoint = new Vector3(0,0,0);

    }

    // Update is called once per frame
    void Update()
    {
        if (start_move == true)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            Vector3 dir = new Vector3(horizontal, 0, vertical);
            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(dir);
                ani.SetInteger("walk", 2);
                transform.Translate(dir * 0.3f * Time.deltaTime, Space.World);
            }
            if(dir == Vector3.zero&&signal==false) { ani.SetInteger("walk", 1); }
        }

        if (last_hitpoint != GameManager.hitpoint&&start_move==true&& GameManager.selectedButton.transform.root.name != "GameManager")
        {
            last_hitpoint = GameManager.hitpoint;
            ani.SetInteger("walk", 2);
            signal = true;
            float one = Mathf.Sqrt(Mathf.Pow(last_hitpoint.x - transform.position.x, 2) + Mathf.Pow(last_hitpoint.z - transform.position.z, 2));
            route = new Vector3((last_hitpoint.x - transform.position.x) / one, 0, (last_hitpoint.z - transform.position.z) / one);
            transform.rotation = Quaternion.LookRotation(route);

        }


        if (signal == true)
        {
            transform.Translate(route * 0.3f * Time.deltaTime, Space.World);
            if ((last_hitpoint - transform.position).magnitude < 0.1)
            {
                ani.SetInteger("walk", 1);
                signal = false;
            }
        }
        if (GameManager.selectedButton && GameManager.selectedButton.name == "model")
        {
            ani.SetInteger("walk", 1);
            GameManager.model_number++;
            if (GameManager.model_number == 4) { GameManager.model_number = 0; }
            creatmodel=Instantiate(GameManager.Instance.model[GameManager.model_number], Camera.main.transform.position + Camera.main.transform.forward, Quaternion.identity);
            creatmodel.transform.parent = GameManager.Instance.transform;
            look_rotation1 = -Camera.main.transform.forward;
            look_rotation2 = new Vector3(look_rotation1.x, 0, look_rotation1.z);
            creatmodel.transform.rotation = Quaternion.LookRotation(look_rotation2);
            //look_rotation = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
            //creatmodel.transform.rotation = Quaternion.LookRotation(-look_rotation);
            GameManager.selectedButton = null;
            Destroy(this.gameObject);
        }

        if (GameManager.selectedButton && GameManager.selectedButton.name == "dance1")
        {
            ani.SetInteger("walk", 3);
            GameManager.selectedButton = this.gameObject;
        }
        if (GameManager.selectedButton && GameManager.selectedButton.name == "dance2")
        {
            ani.SetInteger("walk", 4);
            GameManager.selectedButton = this.gameObject;
        }


        if (ani.GetCurrentAnimatorStateInfo(0).IsName("idle_face_vmd 0"))
        {
            ani.SetInteger("walk", 1);
        }





        start_move = true;


    }
}
