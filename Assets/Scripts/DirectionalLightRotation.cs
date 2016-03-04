using UnityEngine;

public class DirectionalLightRotation : MonoBehaviour
{
    public float RotationSpeedX = -1;


    private GameTime gameTime;

    private void Awake()
    {
        gameTime = GameObject.Find("GlobalScripts").transform.GetComponent<GameTime>();
    }

    private void Update()
    {
        this.transform.Rotate(RotationSpeedX * Time.deltaTime * gameTime.PlayTime, 0, 0);
    }
}