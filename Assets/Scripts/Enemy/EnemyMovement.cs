using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public bool canRun = true;
    public Transform Player;

    private NavMeshAgent myAgent;
	void Awake ()
    {
        myAgent = transform.GetComponent<NavMeshAgent>();
	}
	
	void Update ()
    {
        if (canRun)
        {
            myAgent.SetDestination(Player.position);
        }
	}
}
