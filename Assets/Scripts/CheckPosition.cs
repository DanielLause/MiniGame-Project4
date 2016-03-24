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

public class CheckPosition : MonoBehaviour {

    Transform player;
    Health health;
    void Awake()
    {
        player = transform.Find("BulletSpawn");
        health = player.GetComponent<Health>();
    }
    void Update ()
    {
        if (player.position.y > 200 || player.position.y < 10)
        {
            print("Your position Y is over 200 or unde 10. Look at script \"CheckPosition\"");
            health.isDead = true;
        }
	}
}
