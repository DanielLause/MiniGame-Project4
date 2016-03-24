//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using UnityEngine;

public class ShadowBehavior : MonoBehaviour
{
    [HideInInspector]
    public bool InShadow = false;

    private GameObject objectSun;
    private Transform sun;
    private RaycastHit hit;
    private Animator myStatePattern;

    private void Awake()
    {
        objectSun = GameObject.Find("Directional Light");
        myStatePattern = transform.GetComponent<Animator>();
        sun = objectSun.transform;
    }

    private void Update()
    {
        Physics.Raycast(transform.position, sun.forward * -1, out hit);

        if (hit.transform == null )
        {
            InShadow = false;
            return;
        }
        else
        {
            InShadow = true;
        }
    }
}