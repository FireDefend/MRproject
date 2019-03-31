using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Qmiku : MonoBehaviour {

    // Use this for initialization
    public static Vector3 place_cube;
    public static Vector3 miku_dir;
    public static bool Canvas_sym = false;
    private Rigidbody miku_rigi;
    public static int grade = -10;
    private string grade_text;
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Rigidbody>().drag = 0;
        if (other.name == "Cube" || other.name == "Cube(Clone)")
        {
            grade += 10;
            miku_dir = new Vector3(other.transform.position.x + Random.Range(-0.3f, 0.3f), other.transform.position.y, other.transform.position.z + Random.Range(-0.3f, 0.3f));
            while ((other.transform.position - miku_dir).magnitude < 0.2)
            {
                miku_dir = new Vector3(other.transform.position.x + Random.Range(-0.3f, 0.3f), other.transform.position.y, other.transform.position.z + Random.Range(-0.3f, 0.3f));
            }
            place_cube.x = miku_dir.x;
            place_cube.y = 0f;
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
            grade_text = "You got " + grade + " points\n" + "      try again?";
            transform.root.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = grade_text;
            Canvas_sym = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Cube" || other.name == "Cube(Clone)")
        {
            Destroy(other.gameObject, 1);
        }
           
    }
}
