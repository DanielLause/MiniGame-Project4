using UnityEngine;
using System.Collections;

public class Rotaten : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(0, 5, 0, Space.World);
        transform.localRotation *= Quaternion.Euler(0, 5, 0);
    }
}
