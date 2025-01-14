using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject player;
    public UI ui;
    // Start is called before the first frame update

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.collider;

        if (otherCollider == player.GetComponent<Collider2D>()) // Έλεγχος αν το otherCollider είναι το collider του player
        {
            ui.Victory();
        }
    }
}
