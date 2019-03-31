using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    private float thrust = 0f;
    private bool sym = false;
    private Rigidbody rb;
    private Vector3 J_vector;
    Ray ray;
    RaycastHit hit;

    // Use this for initialization
    void Start ()
    {
        this.transform.Find("Canvas").gameObject.SetActive(false);
        rb = this.transform.Find("Qmiku").GetComponent<Rigidbody>();
    }

// Update is called once per frame
void Update () {


        if (Input.GetMouseButtonDown(0))
        {
            sym = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name == "Yes_button")
                {
                    this.transform.Find("Qmiku").transform.position = Cubemanager.orginal_miku;
                    this.transform.Find("Qmiku").GetComponent<Rigidbody>().drag = 20;
                    this.transform.Find("Cubemanager").transform.Find("Cube(Clone)").transform.position = Cubemanager.orginal_cube;
                    this.transform.Find("Cubemanager").transform.Find("Cube(Clone)").GetComponent<Rigidbody>().drag = 20;
                    this.transform.Find("Canvas").gameObject.SetActive(false);
                    Qmiku.Canvas_sym = false;
                    Qmiku.grade = 0;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            float length = Mathf.Sqrt(Mathf.Pow(Qmiku.miku_dir.x, 2) + Mathf.Pow(Qmiku.miku_dir.z, 2));
            J_vector.x = Qmiku.miku_dir.x / length;
            J_vector.z = Qmiku.miku_dir.z / length;
            J_vector.y = 1f;
            rb.AddForce(J_vector * thrust);
            thrust = 0f;
            sym = false;
        }
        if (sym == true)
        {
            thrust += 2f;
        }

    }


}
