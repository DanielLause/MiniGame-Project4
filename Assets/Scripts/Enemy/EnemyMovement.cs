using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    public bool canRun = true;
    public Transform Player;

    private NavMeshAgent myAgent;
    private GameTime gameTime;

    void Awake()
    {
        myAgent = transform.GetComponent<NavMeshAgent>();
        gameTime = GameObject.Find("GlobalScripts").transform.GetComponent<GameTime>();
    }

    void Update()
    {
        if (canRun && gameTime.PlayTime == 1)
        {
            myAgent.SetDestination(Player.position);
        }
    }
}
