//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using UnityEngine;

public class LookAtCrosshair : MonoBehaviour
{
    [HideInInspector]
    public Vector3 CrosshairTarget;

    public Camera mainCamera;
    private RaycastHit raycastHit;
    private Ray ray;

    private void Awake()
    {
    }

    private void Update()
    {
        var x = Screen.width / 2;
        var y = Screen.height / 2;

        ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        crosshair();
    }

    private void crosshair()
    {
        if (Physics.Raycast(ray, out raycastHit, 150) && raycastHit.transform.gameObject.tag != "Weapon")
            CrosshairTarget = raycastHit.point;
        else
            CrosshairTarget = ray.origin + ray.direction * 150;
    }
}