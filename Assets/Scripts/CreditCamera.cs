using UnityEngine;
using System.Collections;

public class CreditCamera : MonoBehaviour
{

    public int MoveSpeed = 1;
    public int RotateSpeed = 100;

    void Update()
    {
        transform.Rotate(Input.GetAxis("Mouse Y") * RotateSpeed * Time.deltaTime, Input.GetAxis("Mouse X") * RotateSpeed * Time.deltaTime, 0);
        transform.Translate(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime);
    }
}
