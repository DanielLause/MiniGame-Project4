using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public bool ShowLaser;

	public float Damage;
	public float Delay = 0.2f;

	public float RotationSensivity;
	public float MaxYRotation = 100;
	public float MinYRotation = 0;

	//public Transform TransFormToRotateFrom;
	//public GameObject ImpactObject;

	[HideInInspector]
	public bool Shooting = false;



	public GameObject Bullet;
	public Transform SpawnPosition;

	//private ParticleSystem shootParticle;
	//private AudioSource sound;
	private LineRenderer lineRenderer;
	//private bool playSound = false;

	private bool canShoot = true;

	private float rotationY;

	private int delay = 1;
	private float timer;
	private Rigidbody bulletRigid;
	void Awake()
	{
		bulletRigid = Bullet.GetComponent<Rigidbody>();
		lineRenderer = GetComponent<LineRenderer>();
		//shootParticle = GetComponent<ParticleSystem>();
	}

	void Update()
	{
		CheckInput();
		RenderLaser();
		CheckTimer();
	}


	private void CheckTimer()
	{
		float input = Input.GetAxis("Fire1");

		if (input == 1)
		{
			timer += Time.deltaTime;
		}
		else
			timer = 0;
	}

	private void CheckInput()
	{
		float input = Input.GetAxis("Fire1");

		if (input != 0 && canShoot)
		{
			if (Shooting == false)
			{
				Shooting = true;
				StartCoroutine(ShootDelay());
			}
		}
		else
		{
			Shooting = false;
		}
	}
	private void RenderLaser()
	{
		if (ShowLaser)
		{
			Vector3 end = transform.position + (transform.forward.normalized * 100);

			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, end);
		}
	}

	private void Shoot()
	{
		float input = Input.GetAxis("Fire1");

		if (canShoot && input == 1)
		{
			RaycastHit hit = new RaycastHit();

			Ray ray = new Ray(transform.position, transform.forward);
			GameObject bullet = (GameObject)Instantiate(Bullet, SpawnPosition.position,Quaternion.identity);
			bulletRigid.AddForce(Vector3.forward*1000 *Time.deltaTime);

			if (Physics.Raycast(ray, out hit, 1000))
			{
				if (hit.transform.gameObject.GetComponent<Health>() as Health != null)
				{
					GameObject tempG = hit.transform.gameObject;

					tempG.GetComponent<Health>().RemoveHealth(this.Damage);
				}
				else
				{
					//Vector3 tempPos = transform.position - hit.point;

					//Quaternion q = Quaternion.LookRotation(tempPos);

					//Instantiate(ImpactObject, hit.point, q);
				}
			}
		}
	}



	//IEnumerator ShootTimer()
	//{
	//    yield return new WaitForSeconds(Delay);
	//    canShoot = true;
	//    StartCoroutine(ShootTimer());
	//}

	IEnumerator ShootDelay()
	{
		yield return new WaitForSeconds(Delay);
		Shoot();
		StartCoroutine(ShootDelay());
	}
}

