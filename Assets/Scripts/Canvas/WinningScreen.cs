using UnityEngine;
using System.Collections;

public class WinningScreen : MonoBehaviour
{
    GameObject winScreen;

    void Awake()
    {
        winScreen = GameObject.Find("Win_Screen");
        winScreen.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            winScreen.gameObject.SetActive(true);
            
        }
    }
}
