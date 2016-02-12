using UnityEngine;
using System.Collections;

public class TakeItem : MonoBehaviour
{
    [HideInInspector]
    public int ItemCount = 0;
    public string ItemName;

    public void ResetItemCount()
    {
        ItemCount = 0;
    }
    public void ResetItemName()
    {
        ItemName = "";
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            ItemName = other.gameObject.name;
            GameObject.Destroy(other.gameObject);
            ItemCount++;
        }
    }
}
