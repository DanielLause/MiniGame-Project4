using UnityEngine;
using System.Collections;

public class MakeDamage : MonoBehaviour {

    private Shoot shoot;

    Health otherHealth;

    void Awake()
    {
        shoot = (Shoot)GameObject.Find("Player").GetComponentInChildren(typeof(Shoot));
    }
 void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            otherHealth = other.gameObject.GetComponent<Health>();
            otherHealth.RemoveHealth(shoot.Damage);
        }
    }
}
