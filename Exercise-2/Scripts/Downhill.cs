using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downhill : MonoBehaviour
{
    public GameObject player;
    private Collider2D playerC;
    private Rigidbody2D playerRb;
    private float fallSpeed = 6f; 
    private float ignoreDuration = 0.7f; // Διάρκεια για την οποία αγνοούνται οι συγκρούσεις

    void Start()
    {
        playerC = player.GetComponent<Collider2D>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Collider2D otherCollider = collision.collider;

        if (Input.GetKey(KeyCode.S))
        {
            StartCoroutine(TemporaryIgnoreCollision(otherCollider));
            playerRb.velocity = new Vector2(playerRb.velocity.x, -fallSpeed); // Make the player fall
        }
    }

    IEnumerator TemporaryIgnoreCollision(Collider2D otherCollider)
    {
        Physics2D.IgnoreCollision(playerC, otherCollider, true); // Αγνόησε τη σύγκρουση
        yield return new WaitForSeconds(ignoreDuration);          // Περίμενε για τη διάρκεια
        Physics2D.IgnoreCollision(playerC, otherCollider, false); // Επανενεργοποίησε τη σύγκρουση
    }
}
