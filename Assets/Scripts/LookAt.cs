using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    private Transform WinObject;

    private void Update()
    {
        transform.LookAt(WinObject);
    }
}