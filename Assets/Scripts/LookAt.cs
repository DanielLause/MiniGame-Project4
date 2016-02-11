using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour
{

    [SerializeField]
    private Transform WinObject;

    void Update()
    {
        transform.LookAt(WinObject);
    }
}
