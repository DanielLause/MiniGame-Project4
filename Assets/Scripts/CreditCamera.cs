using UnityEngine;
using System.Collections;

public class CreditCamera : MonoBehaviour
{

    Transform parent;
    public int MoveSpeed = 1;
    public int RotateSpeed = 100;

    void Awake()
    {
        parent = transform.parent;
    }
    void Update()
    {
        transform.Rotate(-Input.GetAxis("Mouse Y") * RotateSpeed * Time.deltaTime, 0, 0);
        parent.Rotate(0, Input.GetAxis("Mouse X"), 0, Space.Self);
        transform.Translate(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime);
    }
}
