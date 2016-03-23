﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float MaxHealth = 100;
    public float CurrentHealth;
    public float DestroyTime = 3;
    public bool Player = true;
    public bool isDead = false;
    private bool deadTimer = true;

    private GameObject lose_Screen;

    [SerializeField]
    private GameObject zombieRagdoll;
    [SerializeField]
    private GameObject zombie_walk_cycle;

    void Awake()
    {
        CurrentHealth = MaxHealth;
        zombieRagdoll.SetActive(false);
    }
    void Start()
    {
        if (Player)
        {
        lose_Screen = GameObject.Find("Lose_Screen");
        lose_Screen.SetActive(false);
        }
    }
    void Update()
    {
        if (isDead && deadTimer)
        {
            //Destroy(this.gameObject);
            deadTimer = false;
            StartCoroutine(DestroyThisGameObject(DestroyTime));
            zombie_walk_cycle.SetActive(false);
            zombieRagdoll.SetActive(true);
        }
    }
    public void RemoveHealth(float removeValue)
    {
        CurrentHealth -= removeValue;
        if (CurrentHealth <= 0)
            isDead = true;
    }
    public void AddHealth(float addValue)
    {
        CurrentHealth += addValue;
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }
    public void ReduceMaxHealth(float reduceValue)
    {
        MaxHealth -= reduceValue;
    }
    public void IncreaseMaxHealth(float increaseValue)
    {
        MaxHealth += increaseValue;
    }
    public void SetMaxHealth(float newMaxHealth)
    {
        MaxHealth = newMaxHealth;
    }

    IEnumerator DestroyThisGameObject(float time)
    {
        if (!Player)
        {
            GetComponent<NavMeshAgent>().speed = 0f;
        }
        yield return new WaitForSeconds(time);

        if (Player)
        {
            lose_Screen.gameObject.SetActive(true);
        }


        GameObject.Destroy(this.gameObject);
    }
}
