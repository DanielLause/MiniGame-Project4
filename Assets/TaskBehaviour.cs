using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TaskBehaviour : MonoBehaviour {

    [SerializeField]
    private GameObject TaskTwo;
    [SerializeField]
    private GameObject TaskThree;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TaskTwo.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TaskThree.gameObject.SetActive(false);
        }
    }
}
