using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public GameObject impactParticle = null;
    public GameObject projectileParticle  = null;
    public GameObject[] trailParticles;
    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.
    Transform playerTrans;
    float disappear = 10f;
    // Use this for initialization
    void Start () {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticle.transform.parent = transform;
        playerTrans = GameObject.FindGameObjectWithTag(Strings.Player).transform;
    }
	void Update()
    {
        //Debug.LogError("DIS" + Vector3.Distance(transform.position, playerTrans.position));
        if(Vector3.Distance(transform.position, playerTrans.position) > disappear)
        {
            if(projectileParticle!=null)
                Destroy(projectileParticle, 3f);
            if (impactParticle != null)
                DestroyImmediate(impactParticle, true);
            if (gameObject != null)
                Destroy(gameObject);
        }
    }
	// Update is called once per frame
	void OnCollisionEnter (Collision hit) {
		if (hit.transform.tag==Strings.Player) {
			return;
		}
		if (hit.transform.tag=="Enemy") {
			if (hit.transform.GetComponent<EnemyHealth>().enemyCurHealth<=0) {
				return;
			}

		}
        //transform.DetachChildren();
        if(impactParticle != null)
        {
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
        }
        //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

        if (hit.gameObject.tag == "Destructible") // Projectile will destroy objects tagged as Destructible
        {
            Destroy(hit.gameObject);
        }


        //yield WaitForSeconds (0.05);
        foreach (GameObject trail in trailParticles)
	    {
            //GameObject curTrail = transform.Find(projectileParticle.name + "/" + trail.name).gameObject;
            //curTrail.transform.parent = null;
            //Destroy(curTrail, 3f); 
	    }

        if (projectileParticle != null)
            Destroy(projectileParticle, 3f);
        if (impactParticle != null)
            Destroy(impactParticle, 5f);
        if (gameObject != null)
            Destroy(gameObject);

        //projectileParticle.Stop();

	}
}
