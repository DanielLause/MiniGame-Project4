using UnityEngine;
using System.Collections;

public class DirectionalLightRotation : MonoBehaviour
{

    public float RotationSpeed = -1;

    void Update()
    {
        this.transform.Rotate(RotationSpeed * Time.deltaTime, 0, 0);
    }

}
