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

public class ObstacleSpawn : MonoBehaviour {

    private Transform[] obstacals;

	void Awake () {

        obstacals = transform.Cast<Transform>().ToArray();
        if (obstacals.Length > 1)
                obstacals[Random.Range(0, obstacals.Length)].gameObject.SetActive(false);

    }
	
	void Update ()
    {
	
	}
}
