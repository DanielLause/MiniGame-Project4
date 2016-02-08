using UnityEngine;
using System.Collections;

public class DroneMovement : MonoBehaviour
{
	public Light MyLight;
	public Transform Player;
	public Transform DroneNavAgent;
	public float FlySpeed = 10;
	public float MaxDistance = 3;
	public float FlyUpSpeed = 2;
	public float Height = 2;
	public int LightEffect = 5;

	private Transform player;
	private Rigidbody myRigid;
	NavMeshAgent myAgent;
	private bool canFly = true;
	private bool maxFly = false;
	private int lightEffectTimer = 0;
	bool lightOn = false;

	void Awake()
	{
		player = transform.Find("Player");
		myRigid = transform.GetComponent<Rigidbody>();
		myAgent = DroneNavAgent.GetComponent<NavMeshAgent>();
	}
	void Update()
	{
		fly();
	}

	void fly()
	{
		if (transform.position.y >= Height )
		{
			maxFly = true;
			transform.position = new Vector3(transform.position.x, Height, transform.position.z);
			if (lightEffectTimer <= LightEffect)
			{
				StartCoroutine(lightEffect(0.2f));
				lightEffectTimer++;
			}
			
			myAgent.SetDestination(Player.position);
			//DroneNavAgent.position = new Vector3(DroneNavAgent.position.x, 0.6833334f, DroneNavAgent.position.z);
			
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Q) && canFly)
			{
                canFly = false;
				myRigid.AddForce(0, (FlyUpSpeed * 1000) * Time.deltaTime, 0);
				
			}
		}
	}

	void lightEffect()
	{

	}

	IEnumerator lightEffect(float time)
	{
		MyLight.gameObject.SetActive(false);
		yield return new WaitForSeconds(time);
		MyLight.gameObject.SetActive(true);
	}
}