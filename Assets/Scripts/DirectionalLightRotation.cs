using UnityEngine;

public class DirectionalLightRotation : MonoBehaviour
{
    public float RotationSpeed = -1;
    private GameTime gameTime;

    private void Awake()
    {
        gameTime = GameObject.Find("GlobalScripts").transform.GetComponent<GameTime>();
    }

    private void Update()
    {
        this.transform.Rotate(RotationSpeed * Time.deltaTime * gameTime.PlayTime, 0, 0);
    }
}