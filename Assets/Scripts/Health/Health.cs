﻿using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float MaxHealth = 100;
    public float CurrentHealth;
    public float DestroyTime = 3;
    public bool Player = true;
    public bool isDead = false;
    private bool deadTimer = true;

    //private GameObject lose_Screen;

    void Awake()
    {
        CurrentHealth = MaxHealth;

    }
    void Update()
    {
        if (isDead)
        {
            Destroy(this.gameObject);
            //deadTimer = false;
            //StartCoroutine(DestroyThisGameObject(DestroyTime));
            
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
        yield return new WaitForSeconds(time);

        if (Player)
            //lose_Screen.gameObject.SetActive(true);

        GameObject.Destroy(this);
    }
}
