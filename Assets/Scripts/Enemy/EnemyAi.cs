using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

public class EnemyAi : MonoBehaviour
{
    [Range(0, 10)]
    public float ActivateThreshhold;
    [Range(0, 50)]
    public float Treshhold;
    [Range(1, 50)]
    public float LowTreshhold;
    [Range(0, 1)]
    public float SunThreshold;
    [Range(0, 50)]
    public float RecalcThreshold;

    private Transform waypointContainer;

    private Transform[] wayPoints;
    private Transform player;
    private NavMeshAgent myAgent;
    private GameObject sun;
    private NavMeshPath path;
    private NavMeshHit navHit;
    private RaycastHit rayHit;
    private GameTime gameTime;

    private bool calculatingSun = false;
    private bool inShadow = false;
    private float pathDistance;
    private float sunTime;
    private Vector3 lookTarget;
    

    void Awake()
    {
        gameTime = GameObject.Find("GlobalScripts").GetComponent<GameTime>();
        waypointContainer = GameObject.Find("WaypointContainer").transform;
        sun = GameObject.Find("Directional Light");
        player = GameObject.Find("Player").transform;
        myAgent = GetComponent<NavMeshAgent>();
        wayPoints = waypointContainer.Cast<Transform>().ToArray();
    }
    void Start()
    {
        StartCoroutine(agentFollow());
    }

    IEnumerator getPath(Vector3 target)
    {
        lookTarget = target;
        var waitUpdate = new WaitForEndOfFrame();
        myAgent.SetDestination(target);
        transform.LookAt(lookTarget);
        while (myAgent.pathPending)
            yield return waitUpdate;
        myAgent.Stop();
        path = myAgent.path;
        transform.LookAt(lookTarget);

    }

    IEnumerator agentFollow()
    {
        transform.LookAt(lookTarget);
        if (Vector3.Distance(player.position, transform.position) < ActivateThreshhold)
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(agentFollow());
        }
        else
        {
            yield return StartCoroutine(getPath(player.position));
            yield return StartCoroutine(calcSun());
            yield return StartCoroutine(Run(0));
        }
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

    Vector3 sample(Vector3[] path, float t)
    {
        for (int i = 0; i < path.Length - 1; i++)
        {
            var edge = path[i + 1] - path[i];
            var edgeLength = edge.magnitude;
            if (edgeLength < t)
                t -= edgeLength;
            else
                return Vector3.Lerp(path[i], path[i + 1], t / edgeLength);
        }
        return path[path.Length - 1];
    }

    IEnumerator calcSun()
    {
        calculatingSun = true;

        pathDistance = getLength(path);
        if (pathDistance < Treshhold || (path.corners.Length < 3 && pathDistance < LowTreshhold))
            yield break;

        var step = pathDistance / path.corners.Length;
        var ratio = 1 / step;
        var count = (int)(pathDistance * ratio);
        var sunSamples = new List<Vector3>();
        for (int i = 1; i <= count; i++)
        {
            var sampled = sample(path.corners, i * step);
            if (checkSun(sampled))
            {
                sunSamples.Add(sampled);
            }
        }
        var sunTime = sunSamples.Count / (float)path.corners.Length;
        if (sunTime >= SunThreshold)
        {
            var waypoints = wayPoints.OrderBy(waypoint => (waypoint.position - sunSamples[0]).sqrMagnitude);
            var point = waypoints.FirstOrDefault(wp => !checkSun(wp.position));
            if (point != null)
                yield return StartCoroutine(getPath(point.position));
        }
        calculatingSun = false;
    }

    IEnumerator Run(float initialProgress)
    {
        var fixedUpdateWait = new WaitForFixedUpdate();

        Vector3[] corner = new Vector3[path.corners.Length];
        path.GetCornersNonAlloc(corner);
        var length = getLength(path);
        float progress = initialProgress;
        float breakProgress = 0;
        bool searchingPath = false;
        bool waitingForSun = false;
        while (progress < length - 1)
        {
            yield return fixedUpdateWait;
            progress += myAgent.speed * Time.fixedDeltaTime;
            var movement = Vector3.ClampMagnitude(sample(corner, progress) - transform.position, myAgent.speed * Time.fixedDeltaTime * gameTime.PlayTime);
            myAgent.Move(Vector3.ClampMagnitude(movement, length - progress));
            if (progress >= RecalcThreshold)
            {
                if (!myAgent.pathPending)
                {
                    if (!searchingPath)
                    {
                        searchingPath = true;
                        breakProgress = progress;
                        StartCoroutine(getPath(player.position));
                    }
                    else if (!waitingForSun)
                    {
                        waitingForSun = true;
                        StartCoroutine(calcSun());
                    }
                    else if (!calculatingSun)
                    {
                        StartCoroutine(Run(0));
                        yield break;
                    }
                }
            }
        }

        StartCoroutine(agentFollow());
    }

    private IEnumerator FixPath(Vector3 position)
    {
        var fixedUpdateWait = new WaitForFixedUpdate();

        while (Vector3.Distance(transform.position, position) > 1)
        {
            yield return fixedUpdateWait;
            myAgent.Move(Vector3.ClampMagnitude((position - transform.position).normalized * myAgent.speed * Time.fixedDeltaTime, Vector3.Magnitude(position - transform.position)));

        }
    }

    bool checkSun(Vector3 position)
    {
        Physics.Raycast(position, -sun.transform.forward, out rayHit);

        return rayHit.transform == null;
    }

}
