using UnityEngine;
using System.Collections;

public class ShootDirection : MonoBehaviour {

    [HideInInspector]
    public Vector3 Target;

    RaycastHit hit;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Physics.Raycast(transform.position, transform.forward , out hit);
        //Target = hit.transform.position;

        //if (hit.transform == null || hit.transform.gameObject.tag == "Player")
        //{
        //    return;
        //}
        //else
        //{
        //    Target = hit.transform.position;
        //}
    }
}
