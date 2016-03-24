//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float MinimumXRotation = -90;
    public float MaximumXRotation = 45;

    private Movement playerMovement;
    private Quaternion m_CameraTargetRot;

    private void Awake()
    {
        playerMovement = transform.parent.GetComponent<Movement>();
        m_CameraTargetRot = transform.localRotation;
    }

    private void Update()
    {
        rotation();
    }

    private void rotation()
    {
        float xRot = Input.GetAxis("Mouse Y") * playerMovement.RotateSpeed;
        m_CameraTargetRot *= Quaternion.Euler(-xRot * Time.deltaTime, 0f, 0f);
        m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);
        transform.localRotation = m_CameraTargetRot;
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumXRotation, MaximumXRotation);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}