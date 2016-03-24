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
    public bool InShadow = false;

    [SerializeField, Tooltip("Reduce the enemy movespeed in sun. Take a percent count"), Range(1, 100)]
    private float moveSpeedReduce = 30;
    private GameObject objectSun;
    private Transform sun;
    private RaycastHit hit;
    private Animator myStatePattern;
    private NavMeshAgent myAgent;


    private float oldMoveSpeed;
    private void Awake()
    {
        objectSun = GameObject.Find("Directional Light");
        myStatePattern = transform.GetComponent<Animator>();
        sun = objectSun.transform;
        myAgent = GetComponent<NavMeshAgent>();
        oldMoveSpeed = myAgent.speed;
    }

    private void Update()
    {
        if (!GetComponent<Health>().isDead)
        {
            Physics.Raycast(transform.position, sun.forward * -1, out hit);

            if (hit.transform == null)
            {
                myAgent.speed -= (oldMoveSpeed / 100 * moveSpeedReduce);
                InShadow = false;
                return;
            }
            else
            {
                InShadow = true;
                myAgent.speed = oldMoveSpeed;
            }
        }
    }
}