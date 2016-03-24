using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100;
    public float CurrentHealth;
    public float DestroyTime = 3;
    public bool Player = true;
    public bool isDead = false;

    private bool deadTimer = true;
    private GameObject lose_Screen;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    private void Start()
    {
        if (Player)
        {
            lose_Screen = GameObject.Find("Lose_Screen");
            lose_Screen.SetActive(false);
        }
    }

    private void Update()
    {
        if (isDead && deadTimer)
        {
            deadTimer = false;
            StartCoroutine(DestroyThisGameObject(DestroyTime));
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

    private IEnumerator DestroyThisGameObject(float time)
    {
        if (!Player)
        {
            transform.FindChild("zombie_walk_cycle").gameObject.SetActive(false);
            transform.FindChild("zombie_Ragdoll_final").gameObject.SetActive(true);
            GetComponent<NavMeshAgent>().speed = 0f;
            GetComponent<Attack>().Damage = 0;
        }
        yield return new WaitForSeconds(time);

        if (Player)
            lose_Screen.gameObject.SetActive(true);

        GameObject.Destroy(this.gameObject);
    }
}