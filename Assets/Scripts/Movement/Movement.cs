using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public int MoveSpeed = 1;
    public int RotateSpeed = 100;
    
	void Start ()
    {
	}
	
	void Update ()
    {
        Move();
        transform.Rotate(0, Input.GetAxis("Mouse X")*RotateSpeed*Time.deltaTime, 0);
    }

    private void Move()
    {
        transform.Translate(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime);

    }
}
