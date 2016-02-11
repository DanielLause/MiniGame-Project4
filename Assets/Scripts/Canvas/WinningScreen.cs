using UnityEngine;
using System.Collections;

public class WinningScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject winCamera;
    [SerializeField]
    private GameObject winScreen;

    private bool Win = false;

    void Awake()
    {
        winCamera.SetActive(false);
        winScreen.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(mainCamera);
            winScreen.SetActive(true);
            winCamera.SetActive(true);
            Win = true;
        }
    }

    void Update()
    {
        if (Win)
        {
            this.gameObject.transform.Translate(0, 2, 0);
        }
    }
}
