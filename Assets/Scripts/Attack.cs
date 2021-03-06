﻿//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    public int Damage = 1;
    Health otherHealth;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            otherHealth = other.gameObject.GetComponent<Health>();
            otherHealth.RemoveHealth(Damage);
        }
    }
}
