using UnityEngine;
using System.Collections;

public class TaskOne : MonoBehaviour
{
    [SerializeField]
    private GameObject taskOne;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            this.gameObject.SetActive(false);
            taskOne.SetActive(false);
        }
    }
}
