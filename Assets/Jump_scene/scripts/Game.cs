using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    private float thrust = 0f;
    private bool sym = false;
    private Rigidbody rb;
    private Vector3 J_vector;
    // Use this for initialization
    void Start () {
        rb = this.transform.Find("Qmiku").GetComponent<Rigidbody>();
    }

// Update is called once per frame
void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            sym = true;
       
        }
        if (Input.GetMouseButtonUp(0))
        {
            float length = Mathf.Sqrt(Mathf.Pow(Cube.miku_dir.x, 2) + Mathf.Pow(Cube.miku_dir.z, 2));
            J_vector.x = Cube.miku_dir.x / length;
            J_vector.z = Cube.miku_dir.z / length;
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
