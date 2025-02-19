using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Vector3 dPos; // Μετατόπιση θέσης
    [SerializeField] private float duration = 1f; // Διάρκεια κίνησης
    private bool open;

    public void Operate()
    {
        if (open)
        {
            CloseDoor(); // Κλείσιμο
        }
        else
        {
            OpenDoor(); // Άνοιγμα
        }

        open = !open; // Εναλλαγή κατάστασης
    }

    public void Activate()
    {
        if (!open)
        {
            OpenDoor();
            open = true;
        }
    }

    public void Deactivate()
    {
        if (open)
        {
            CloseDoor();
            open = false;
        }
    }

    private void OpenDoor()
    {
        // Μετακίνηση ομαλά στην ανοιχτή θέση
        transform.DOMove(transform.position + dPos, duration).SetEase(Ease.OutQuad);
    }

    private void CloseDoor()
    {
        // Μετακίνηση ομαλά στην κλειστή θέση
        transform.DOMove(transform.position - dPos, duration).SetEase(Ease.OutQuad);
    }
}
