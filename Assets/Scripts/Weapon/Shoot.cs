using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //public GameObject BulletHostPrefab;
    public GameObject BulletTargetPrefab;
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
    public float Damage = 10;
    public bool CanShoot = true;
    public bool Reload = false;

    [HideInInspector]
    public Vector3 DroneTarget;

    private GameObject newBulletTarget;
    private GameObject bulletHost;
    //public Transform bulletTarget;
    private GameObject bulletContainer;
    public LighteningScript myLightScript;

    //private Rigidbody myRigidbody;
    private RaycastHit raycastHit;

    private int bulletAmount = 0;
    private bool shoot = false;


    private void Awake()
    {
        bulletContainer = new GameObject(transform.name + "_bullets");
        bulletContainer.transform.SetParent(transform);
    }

    private void Update()
    {
        droneTarget();
        //CooldownWeapon();
        bulletSpawn();
    }
    private void FixedUpdate()
    {

        transform.LookAt(LookAtCrosshairScript.CrosshairTarget);
    }

    private void bulletSpawn()
    {
        droneTarget();
        if (CanShoot && !Reload)
        {

            if (Input.GetAxis("Fire1") > 0.1f || Input.GetMouseButton(0))
            {
                //ShotSound.Stop();
                //ShotSound.Play();

                newBulletTarget = (GameObject)Instantiate(BulletTargetPrefab, BulletSpawn.transform.position, BulletSpawn.transform.rotation);
                newBulletTarget.transform.SetParent(bulletContainer.transform);

                if (newBulletTarget.gameObject != null)
                {
                    myLightScript.enabled = true;
                    myLightScript.shoot = true;
                    myLightScript.gameObject.SetActive(true);
                    myLightScript.enabled = true;
                    myLightScript.TargetGameObject = newBulletTarget.gameObject;
                }
                else
                {
                    myLightScript.enabled = false;
                    myLightScript.gameObject.SetActive(false);

                }

                Rigidbody bulletRig = newBulletTarget.GetComponent<Rigidbody>();
                bulletRig.AddForce(transform.forward * (BulletForce * 2000) * Time.deltaTime);

                bulletAmount++;
                StartCoroutine(destroyAfterLifetime(newBulletTarget));
                StartCoroutine(shootDelay());
            }

            shoot = false;
        }
    }

    private void droneTarget()
    {
        Ray ray = new Ray(BulletSpawn.position, BulletSpawn.forward);
        Physics.Raycast(ray, out raycastHit, DroneMovementScript.MaxDistance * 10000);
        if (raycastHit.transform.localPosition != null)
        {
            DroneTarget = raycastHit.point;
        }
    }

    private void CooldownWeapon()
    {
        if (bulletAmount == MaxBulletAmount && !Reload)
        {
            StartCoroutine(weaponCooldown());
        }
    }

    private IEnumerator weaponCooldown()
    {
        Reload = true;
        CanShoot = false;
        yield return new WaitForSeconds(ReloadTime);
        Reload = false;
        CanShoot = true;
        bulletAmount = 0;
    }

    private IEnumerator destroyAfterLifetime(GameObject mybullet)
    {
        yield return new WaitForSeconds(LifeTime);
        myLightScript.enabled = false;
        myLightScript.shoot = false;
        myLightScript.gameObject.SetActive(false);
        Destroy(mybullet);
    }

    private IEnumerator shootDelay()
    {
        CanShoot = false;
        yield return new WaitForSeconds(Random.Range((float)MinDelayTime, (float)MaxDelayTime));
        CanShoot = true;
    }
}