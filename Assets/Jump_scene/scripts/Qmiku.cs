using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Qmiku : MonoBehaviour {

    // Use this for initialization
    public static Vector3 place_cube;
    public static Vector3 miku_dir;
    public static bool Canvas_sym;
    private Rigidbody miku_rigi;
    public static int grade;
    private string grade_text;
    private string realtime_grade_text;
    private bool trigger_enter;
    private int trigger_count;
    public static Animator Qmiku_ani;
    private bool stay_sym;
    private GameObject repeat_item;
    private bool start_sym;
    private int stay_count;
    void Start() {
        SceneManager.UnloadSceneAsync("loading");
        stay_count = 0;
        stay_sym = false;
        start_sym = false;
        repeat_item = null;
        Canvas_sym = false;
        trigger_enter = false;
        trigger_count = 0;
        grade = 0;
        Qmiku_ani = GetComponent<Animator>();
        Qmiku_ani.SetInteger("motion", 2);
        this.transform.root.transform.Find("Grade_screen").transform.Find("detect_text").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        transform.root.Find("Grade_screen").transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        transform.root.Find("Grade_screen").transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        realtime_grade_text = grade + " points\n";
        transform.root.Find("Grade_screen").transform.Find("Text").GetComponent<Text>().text = realtime_grade_text;
        if(Vector3.Distance(this.transform.position,Camera.main.transform.position)>50)
        {
             transform.root.Find("Canvas").gameObject.SetActive(true);
             transform.root.Find("Cursor").gameObject.SetActive(true);
             grade_text = "You got " + grade + " points\n" + "      try again?";
             transform.root.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = grade_text;
             Canvas_sym = true;
             this.transform.position = Cubemanager.orginal_miku;

          
        }



    }
    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Rigidbody>().drag = 0;
        trigger_enter = true;
        trigger_count = 0;
        Qmiku_ani.SetInteger("motion", 2);
        if (repeat_item != other.gameObject)
        {

            if (other.name == "Cube" || other.name == "Cube(Clone)" || other.name == "tri 1(Clone)" || other.name == "Cylinder(Clone)")
            {
                if (repeat_item)
                {
                    if (repeat_item.name == "Cube" || repeat_item.name == "Cube(Clone)" || repeat_item.name == "tri 1(Clone)" || repeat_item.name == "Cylinder(Clone)")
                    {
                        Destroy(repeat_item.gameObject);
                    }
                }
                miku_dir = new Vector3(other.transform.position.x + Random.Range(-0.3f, 0.3f), other.transform.position.y, other.transform.position.z + Random.Range(-0.3f, 0.3f));
                while ((other.transform.position - miku_dir).magnitude < 0.2)
                {
                    miku_dir = new Vector3(other.transform.position.x + Random.Range(-0.3f, 0.3f), other.transform.position.y, other.transform.position.z + Random.Range(-0.3f, 0.3f));
                }
                place_cube.x = miku_dir.x;
                place_cube.y = this.transform.position.y + 0.1f;
                place_cube.z = miku_dir.z;
                miku_dir.y = this.transform.position.y;
                miku_dir = miku_dir - this.transform.position;
                this.transform.rotation = Quaternion.LookRotation(miku_dir);
                //this.transform.parent.Find("Qmiku").transform.forward = (miku_dir - this.transform.position);
                Cubemanager.create_sym = true;

            }
            else
            {
                transform.root.Find("Canvas").gameObject.SetActive(true);
                transform.root.Find("Cursor").gameObject.SetActive(true);
                transform.root.Find("Canvas").transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
                grade_text = "You got " + grade + " points\n" + "      try again?";
                transform.root.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = grade_text;
                Canvas_sym = true;
            }
            repeat_item = other.gameObject;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        trigger_count++;
        if(trigger_enter==true&&trigger_count>20)
        {
            if (other.name == "Cube" || other.name == "Cube(Clone)" || other.name == "tri 1(Clone)" || other.name == "Cylinder(Clone)")
            {
                if (other.name == "Cube(Clone)") { grade += 20; this.transform.root.Find("Grade_screen").transform.Find("Text").gameObject.SetActive(true); }
                else if (other.name == "tri 1(Clone)") { grade += 30; }
                else if (other.name == "Cylinder(Clone)") { grade += 10; }
                else if (other.name == "Cube") { grade = 0; }

            }
            trigger_enter = false;
            trigger_count = 0;
        }
        stay_sym = true;
        start_sym = true;
    }
 
}
