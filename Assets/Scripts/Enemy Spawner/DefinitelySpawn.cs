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

public class DefinitelySpawn : MonoBehaviour {
    [SerializeField]
    private Spawner mySpawner;

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            mySpawner.DefinitelySpawn = true;
        }
    }
}
