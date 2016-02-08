using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

    private Movement playerMovement;
	
    void Awake()
    {
        playerMovement = transform.parent.GetComponent<Movement>();
    }
	void Update ()
    {
        rotation();
	}
    void rotation()
    {
            transform.Rotate(Input.GetAxis("Mouse Y") * playerMovement.RotateSpeed * Time.deltaTime, 0, 0);
    }
}
