using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesDamage : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRb;
    private Collider2D playerC;
    private float deathRotation = -15f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        playerC = player.GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) // When the player hits the spikes :((
    {
        Collider2D otherCollider = collision.collider;

        if (otherCollider == playerC) // Έλεγχος αν το otherCollider είναι το collider του player
        {
            playerRb.rotation = deathRotation;
            playerC.enabled = false; // Aπενεργοποιηση του collider του παίχτη ώστε να πέφτει
        }
    }
}