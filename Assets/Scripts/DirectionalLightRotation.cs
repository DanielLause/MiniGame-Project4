using UnityEngine;
using System.Collections;

public class DirectionalLightRotation : MonoBehaviour
{

    public float RotationSpeed = -1;
    private GameTime gameTime;

    void Awake()
    {
        gameTime = GameObject.Find("GlobalScripts").transform.GetComponent<GameTime>();
    }

    void Update()
    {
        this.transform.Rotate(RotationSpeed * Time.deltaTime * gameTime.PlayTime, 0, 0);
    }

}
