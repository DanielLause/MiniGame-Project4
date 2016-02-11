using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	public float MinimumXRotation = -90;
	public float MaximumXRotation = 45;

	private Movement playerMovement;
	private Quaternion m_CameraTargetRot;
    void Awake()
    {
        playerMovement = transform.parent.GetComponent<Movement>();
		m_CameraTargetRot = transform.localRotation;

    }
	void Update ()
    {
		rotation();
	}
	void rotation()
	{
		float xRot = Input.GetAxis("Mouse Y") * playerMovement.RotateSpeed;
		m_CameraTargetRot *= Quaternion.Euler(-xRot * Time.deltaTime, 0f, 0f);
		m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);
		transform.localRotation = m_CameraTargetRot;
	}
	Quaternion ClampRotationAroundXAxis(Quaternion q)
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
