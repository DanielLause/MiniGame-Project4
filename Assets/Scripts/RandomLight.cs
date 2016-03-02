using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RandomLight : MonoBehaviour {

    private Transform[] lanterns;
    private List<GameObject> lanternLights;
    private List<GameObject> deactivLanternLights;

    void Awake()
    {
        lanternLights = new List<GameObject>();
        deactivLanternLights = new List<GameObject>();
        lanterns = transform.Cast<Transform>().ToArray();
        foreach (var lantern in lanterns)
        {
            lanternLights.Add((lantern.FindChild("Light").gameObject));
        }
    }

    void Start()
    {
        int randomLantenrOnCount = Random.Range(1, lanternLights.Count);
        for (int i = randomLantenrOnCount; i <= lanternLights.Count; i++)
        {
            int r = Random.Range(0, lanternLights.Count);

            lanternLights[r].SetActive(false);
            deactivLanternLights.Add(lanternLights[r]);
            lanternLights.Remove(lanternLights[r]);
        }
    }



}
