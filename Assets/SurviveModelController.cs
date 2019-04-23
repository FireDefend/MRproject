using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Academy.HoloToolkit.Unity;
using UnityEngine.SceneManagement;
public class SurviveModelController : MonoBehaviour {


    Animation ani;
    Animator animator;
    Animation weaponAni;
    AnimationClip idleClip;
    AnimationClip runClip;
    AnimationClip getHurtClip;
    AnimationClip shootClip;
    [HideInInspector]
    public AnimationClip deathClip;
    [HideInInspector]
    public int scoreNum;
    public GameObject scoreText;
    AnimationClip weaponIdleClip;
    AnimationClip weaponRunClip;
    AnimationClip weaponDeathClip;
    public float moveSpeed = 2;
    PlayerFireBullet weapon;
    float x, z;
    int layerMask;
    void Awake()
    {

        //ani = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        weapon = GetComponent<PlayerFireBullet>();
        
    }

    void Start()
    {
        //deathClip = ani.GetClip("head_vmd");
        //shootClip = ani.GetClip("a2o_pathObstacles_clearWithMachete_all loop1_vmd");
        //runClip = ani.GetClip("walk1010_vmd");
        //idleClip = ani.GetClip("idle_face_vmd");

        layerMask = LayerMask.GetMask("Ground");
        SceneManager.UnloadSceneAsync("loading2");
    }

    void FixedUpdate()
    {
            Move();
            Rotate();
        if(scoreText != null)
        {
            scoreText.GetComponent<TextMesh>().text = "score: " + scoreNum;
        }
        
    }


    void Move()
    {

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        if (x != 0 || z != 0)
        {
            animator.SetInteger("state", 1);
            transform.Translate(new Vector3(x, 0, z) * Time.deltaTime * moveSpeed, Space.World);
            //ani.clip = runClip;
            //ani.Play();
            
        }
        else
        {
            //ani.clip = idleClip;
            //ani.Play();
            animator.SetInteger("state", 2);

        }


        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;


        //Ray ray= Camera.main.ScreenPointToRay (Input.mousePosition);
        var ray = new Ray(headPosition, gazeDirection);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 120, layerMask))
        {

            Vector3 lookPos = hitInfo.point - transform.position;
            transform.rotation = Quaternion.LookRotation(lookPos);
        }


    }



    void Rotate()
    {





    }





}

