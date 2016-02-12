using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject BulletPrefab;
	public Transform BulletSpawn;
	public DroneMovement DroneMovementScript;
    public LookAtCrosshair LookAtCrosshairScript;
	//public AudioSource ShotSound;
	public float BulletForce = 100;
	public float LifeTime = 1;
	public float MaxDelayTime = 0.6f;
	public float MinDelayTime = 0.4f;
	public int MaxBulletAmount = 100;
	public float ReloadTime = 1;
	public bool CanShoot = true;
	public bool Reload = false;
	[HideInInspector]
	public Vector3 DroneTarget;

	private GameObject newBullet;
	//private Rigidbody myRigidbody;
	private RaycastHit raycastHit;
	private int bulletAmount = 0;

	void Update()
	{
		droneTarget();
		//CooldownWeapon();
		bulletSpawn();
        transform.LookAt(LookAtCrosshairScript.CrosshairTarget);
	}

	void bulletSpawn()
	{
		if (CanShoot && !Reload)
		{
			if (Input.GetAxis("Fire1") > 0.1f || Input.GetMouseButton(0))
			{
				//ShotSound.Stop();
				//ShotSound.Play();

				newBullet = (GameObject)Instantiate(BulletPrefab, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
				Rigidbody bulletRig = newBullet.GetComponent<Rigidbody>();
				bulletRig.AddForce(transform.forward * (BulletForce* 2000) * Time.deltaTime);
				bulletAmount++;
				StartCoroutine(destroyAfterLifetime(newBullet));
				StartCoroutine(shootDelay());
			}
		}
	}
	private void droneTarget()
	{
		Ray ray = new Ray(BulletSpawn.position, BulletSpawn.forward);
		Physics.Raycast(ray,out raycastHit,DroneMovementScript.MaxDistance * 10000);
		if (raycastHit.transform.localPosition != null )
		{
			DroneTarget = raycastHit.point;
		}
	}
	void CooldownWeapon()
	{
		if (bulletAmount == MaxBulletAmount && !Reload)
		{
			StartCoroutine(weaponCooldown());
		}
	}
	IEnumerator weaponCooldown()
	{
		Reload = true;
		CanShoot = false;
		yield return new WaitForSeconds(ReloadTime);
		Reload = false;
		CanShoot = true;
		bulletAmount = 0;
	}
	IEnumerator destroyAfterLifetime(GameObject mybullet)
	{
		yield return new WaitForSeconds(LifeTime);
		Destroy(mybullet);
	}
	IEnumerator shootDelay()
	{
		CanShoot = false;
		yield return new WaitForSeconds(Random.Range((float)MinDelayTime, (float)MaxDelayTime));
		CanShoot = true;
	}
}
