using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] string itemName;
    public AudioSource collect;

    void OnTriggerEnter(Collider other)
    {
        collect.Play(); // play sound
        Managers.Inventory.AddItem(itemName);
        Destroy(this.gameObject);
    }

}
