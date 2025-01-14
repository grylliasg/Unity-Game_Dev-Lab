using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D playerRb;
    private float trampolineJump = 15f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.collider;

        if (otherCollider == player.GetComponent<Collider2D>()) // Έλεγχος αν το otherCollider είναι το collider του player
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, trampolineJump); // Speed προς τα πάνω 
        }
    }
}
