using UnityEngine;
using System.Collections;

public class waffentest : MonoBehaviour {

    public ShootDirection myDirection;
    void Update()
    {
        transform.LookAt(myDirection.Target);
    }
       
}
