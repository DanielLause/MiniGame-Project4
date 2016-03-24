//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ObstacleSpawn : MonoBehaviour {

    private Transform[] obstacals;
    private List<Transform> tempObstacals;

	void Awake () {

        obstacals = transform.Cast<Transform>().ToArray();
        tempObstacals = new List<Transform>();
        foreach (var item in obstacals)
        {
            tempObstacals.Add(item);
        }
        if (tempObstacals.Count > 0)
        {

        for (int i = 0; i < tempObstacals.Count+1; i++)
        {
                int k = Random.Range(0, tempObstacals.Count);
                tempObstacals[k].gameObject.SetActive(true);
                tempObstacals.Remove(tempObstacals[k]);
        }
        }

    }
	
	void Update ()
    {
	
	}
}
