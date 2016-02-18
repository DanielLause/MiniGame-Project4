using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

public class EnemyAi : MonoBehaviour
{
    [Range(0, 50)]
    public float Treshhold;
    [Range(1, 50)]
    public float LowTreshhold;

    private Transform waypointContainer;

    private Transform[] wayPoints;
    private Transform player;
    private NavMeshAgent myAgent;
    private GameObject sun;
    private NavMeshPath path;
    private NavMeshHit navHit;
    private RaycastHit rayHit;

    private float sunTime;
    private bool inShadow = false;
    private float pathDistance;
    void Awake()
    {
        waypointContainer = GameObject.Find("WaypointContainer").transform;
        sun = GameObject.Find("Directional Light");
        player = GameObject.Find("Player").transform;
        myAgent = GetComponent<NavMeshAgent>();
        wayPoints = waypointContainer.Cast<Transform>().ToArray();
    }
    void Start()
    {

        agentFollow();
    }

    void getPath()
    {
        path = new NavMeshPath();
        myAgent.CalculatePath(player.position, path);

    }
    void agentFollow()
    {
        getPath();
        calcSun();
        StartCoroutine(Run());
    }
    float getLength(NavMeshPath path)
    {
        float length = 0;
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            length += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }
        return length;
    }
    Vector3 sample(NavMeshPath path, float t)
    {
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            var edge = path.corners[i + 1] - path.corners[i];
            var edgeLength = edge.magnitude;
            if (edgeLength < t)
                t -= edgeLength;
            else
                return Vector3.Lerp(path.corners[i], path.corners[i + 1], t / edgeLength);
        }
        return path.corners[path.corners.Length - 1];
    }
    void calcSun()
    {
        var corner = new Vector3[path.corners.Length];
        path.GetCornersNonAlloc(corner);

        pathDistance = getLength(path);
        if (pathDistance < Treshhold || (path.corners.Length < 3 && pathDistance < LowTreshhold))
            return;

        var ratio = pathDistance / path.corners.Length;
        var count = (int)(pathDistance * ratio);
        var step = 1 / ratio;
        for (int i = 1; i < count; i++)
        {
            var sampled = sample(path, i * step);
            if (checkSun(sampled))
            {
                print("i found sun");
            }
        }
    }
    IEnumerator Run()
    {
        var fixedUpdateWait = new WaitForFixedUpdate();
        var length = getLength(path);
        float progress = 0;
        while (progress < length)
        {
            yield return fixedUpdateWait;
            progress += myAgent.speed * Time.fixedDeltaTime;
            myAgent.Move(Vector3.ClampMagnitude((sample(path, progress) - transform.position), length - progress));
        }
    }
    bool checkSun(Vector3 position)
    {
        Physics.Raycast(position, -sun.transform.forward, out rayHit);

        return rayHit.transform == null;
    }
    void anyFunction()
    {
        List<Vector3> wayPoints = new List<Vector3>();
        for (int i = 1; i < path.corners.Length; i++)
        {
            wayPoints.Clear();
            //divide(path.corners[i - 1], path.corners[i], wayPoints);
            foreach (var wayPoint in wayPoints)
            {
                if (checkSun(wayPoint))
                {

                }
            }
        }
    }
    //void divide(Vector3 start, Vector3 end, List<Vector3> wayPoints)
    //{
    //    wayPoints.Add(start);
    //    wayPoints.Add((start + end) / 2);
    //    wayPoints.Add(end);

    //}
}
