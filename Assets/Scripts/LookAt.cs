//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

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