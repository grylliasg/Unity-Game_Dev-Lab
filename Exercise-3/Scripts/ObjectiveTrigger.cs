using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectiveTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        int itemCount = Managers.Inventory.GetItemCount("itemName");
        Debug.Log(itemCount);
        Managers.Mission.ReachObjective();
    }
}