using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public DoorOpenDevice door;
    public GameObject player;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        int itemCount = Managers.Inventory.GetItemCount("key");
        // Ελέγχει αν το αντικείμενο που μπήκε στο trigger είναι ο παίκτης
        if (other.gameObject == player && itemCount != 0)
        {
            door.Activate(); // Άνοιγμα της πόρτας
        }
        else
        {
            Debug.Log("You Need the Key");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Ελέγχει αν το αντικείμενο που βγήκε από το trigger είναι ο παίκτης
        if (other.gameObject == player)
        {
            door.Deactivate(); // Κλείσιμο της πόρτας
        }
    }
}
