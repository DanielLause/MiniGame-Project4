using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {

    public int Damage = 1;
    Health otherHealth;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("PLAYER");
            otherHealth = other.gameObject.GetComponent<Health>();
            otherHealth.RemoveHealth(Damage);
        }
    }
}
