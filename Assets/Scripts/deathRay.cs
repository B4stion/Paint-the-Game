using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathRay : MonoBehaviour {

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    private Vector3 origin;
    private Vector3 direction;

    private float currentHitDistance;
    public GameObject currentHitObject;

    public GameObject eye;
    private GameObject player;
    private Vector3 playerPos;

	// Use this for initialization
	void Start () {
        eye = this.gameObject.transform.GetChild(0).gameObject;
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        maxDistance = 50f;
        sphereRadius = 3f;
    }

    // Update is called once per frame
    void Update () {
        origin = transform.position;
        direction = transform.forward;
        RaycastHit hit;
        RaycastHit hit2;
        if(Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            Light lt = eye.GetComponent<Light>();

            if (Physics.Raycast(origin, direction, out hit2, Mathf.Infinity, layerMask))
            {
                lt.range = hit2.distance+2;
            }

            //lt.range = currentHitDistance+sphereRadius+2;
            
        }
        else
        {
            currentHitDistance = maxDistance;
            
            currentHitObject = null;
        }

        //print(currentHitObject);

        if (currentHitObject != null)
        {
            if (currentHitObject.name == "Player")
            {
                print("Hit Player");
                player.transform.position = playerPos;
            }
        }
        


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
