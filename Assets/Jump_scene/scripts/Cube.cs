using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
    public static bool create_sym;
    public static Vector3 place_cube;
    public static Vector3 miku_dir;

    // Use this for initialization
    void Start () {
     create_sym = false;


}
	
	// Update is called once per frame
	void Update ()
    {
		 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.position.y > this.transform.position.y)
        {
            miku_dir = new Vector3(this.transform.position.x + Random.Range(-0.3f, 0.3f), this.transform.position.y, this.transform.position.z + Random.Range(-0.3f, 0.3f));
            while ((this.transform.position - miku_dir).magnitude < 0.2)
            {
                miku_dir = new Vector3(this.transform.position.x + Random.Range(-0.3f, 0.3f), this.transform.position.y, this.transform.position.z + Random.Range(-0.3f, 0.3f));
            }
            place_cube.x = miku_dir.x;
            place_cube.y = 5f;
            place_cube.z = miku_dir.z;
            miku_dir.y = this.transform.root.Find("Qmiku").transform.position.y;
            miku_dir = miku_dir - this.transform.root.Find("Qmiku").transform.position;
            this.transform.root.Find("Qmiku").transform.rotation = Quaternion.LookRotation(miku_dir);
            //this.transform.parent.Find("Qmiku").transform.forward = (miku_dir - this.transform.position);
            Cubemanager.create_sym = true;

        }
      
      
 
    }
    private void OnTriggerExit(Collider other)
    {
          if(other.transform.position.y < this.transform.position.y)
        {

            this.transform.root.Find("Qmiku").transform.position = Cubemanager.orginal_miku;
            this.transform.position = Cubemanager.orginal_cube;

        }
        else
        {
            Destroy(this.gameObject, 1);
        }

    }
}
