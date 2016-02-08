using UnityEngine;
using System.Collections;

public class CheckPosition : MonoBehaviour {

    //Transform player;
    Health health;
    void Awake()
    {
        //player = transform.Find("Player");
        health = transform.GetComponent<Health>();
    }
    void Update ()
    {
        if (transform.position.y > 200 || transform.position.y < -10)
        {
            print("Your position Y is over 200 or under -10. Look at script \"CheckPosition\"");
            health.isDead = true;
        }
	}
}
