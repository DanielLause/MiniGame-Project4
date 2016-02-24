using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HP_Refresh : MonoBehaviour
{

    private Health health;

    [SerializeField]
    private Text text;

    void Awake()
    {
        health = GameObject.Find("Player").GetComponent<Health>();
    }

    void Update()
    {
        text.text = health.CurrentHealth.ToString();
    }
}
