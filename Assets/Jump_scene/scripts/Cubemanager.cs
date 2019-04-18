using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubemanager : MonoBehaviour
{
    private GameObject new_cube;
    private int time_count;
    public GameObject[] model_cube;
    public static bool create_sym;
    public static Vector3 orginal_miku;
    public static Vector3 orginal_cube;
    // Use this for initialization
    void Start()
    {
        time_count = 61;
        create_sym = false;
        orginal_miku=this.transform.root.Find("Qmiku").transform.position;
        orginal_cube=this.transform.Find("Cube").transform.position;
}

    // Update is called once per frame
    void Update()
    {
        time_count++;
        if (time_count > 10)
        {
            if (create_sym == true)
            {
                time_count = 0;
                new_cube = Instantiate(model_cube[Random.Range(0, 3)], Qmiku.place_cube, Quaternion.identity);
                new_cube.transform.parent = this.transform;
                create_sym = false;
            }
        }
        else { create_sym = false; }
                  
       


    }




}
