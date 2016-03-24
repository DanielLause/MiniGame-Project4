//////////////////////////////////////////////
///                                        ///
///         Last Day - Source Code         ///
///                                        ///
///    Lead Programmer   Second Programmer ///
///      Daniel Lause       Julian Hopp    ///
///                                        ///
//////////////////////////////////////////////

using System.Collections;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    public string ItemName;

    [HideInInspector]
    public int ItemCount = 0;

    [SerializeField, Tooltip("Healed the player. You can't take a \"OverHeal\".")]
    private int mediPackHealth = 10;

    [SerializeField, Tooltip("Gives the player more movespeed. Take a percent count"), Range(1, 100)]
    private float drugPotSpeed = 20;

    [SerializeField, Tooltip("Take a timer for DrugPot effect.")]
    private float drugPotEffectTimer = 10;

    private float oldMoveSpeed;
    private float oldRunSpeed;
    private bool drugEffectActive = false;

    private void Start()

    {
        oldMoveSpeed = GetComponent<Movement>().MoveSpeed;
        oldRunSpeed = GetComponent<Movement>().RunSpeed;
    }

    public void ResetItemCount()
    {
        ItemCount = 0;
    }

    public void ResetItemName()
    {
        ItemName = "";
    }

    private IEnumerator drugPotTimer(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Movement>().MoveSpeed = oldMoveSpeed;
        GetComponent<Movement>().RunSpeed = oldRunSpeed;
        drugEffectActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Medipack")
        {
            if (GetComponent<Health>().CurrentHealth < GetComponent<Health>().MaxHealth)
            {

            ItemName = other.gameObject.name;
            GetComponent<Health>().AddHealth(mediPackHealth);
            GameObject.Destroy(other.gameObject);
            ItemCount++;
            }
        }
        if (other.gameObject.tag == "Drugpot")
        {
            if (!drugEffectActive)
            {

            ItemName = other.gameObject.name;
            GetComponent<Movement>().MoveSpeed += (oldMoveSpeed / 100 * drugPotSpeed);
            GetComponent<Movement>().RunSpeed += (oldRunSpeed / 100 * drugPotSpeed);
            GameObject.Destroy(other.gameObject);
                ItemCount++;
                drugEffectActive = true;
                StartCoroutine(drugPotTimer(drugPotEffectTimer));
            }
        }
        if (other.gameObject.tag == "Pointer")
        {
            ItemName = other.gameObject.name;
            GameObject.Destroy(other.gameObject);
            ItemCount++;
        }
    }
}