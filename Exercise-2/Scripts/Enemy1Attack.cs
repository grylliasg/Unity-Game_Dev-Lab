using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy1Attack : MonoBehaviour
{
    private GameObject player;
    private Collider2D playerC;
    private Rigidbody2D playerRb;
    private Vector2 movement;
    public float enemySpeed = .3f;
    private Animator anim;
    private float enemyX;
    private int direction = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyX = transform.position.x;
        player = GameObject.Find("Player");
        playerC = player.GetComponent<Collider2D>();
        playerRb = player.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyX += enemySpeed * direction * Time.deltaTime; 
        movement = new Vector2(enemyX, transform.parent.position.y + transform.GetComponent<Collider2D>().bounds.extents.y); 
        // Στον άξονα Υ υπολογίζουμε την μιση απόσταση του Collider του enemy για να το ανυψώσοουμε κατάλληλα
        
        transform.position = movement;

        if (((direction == 1) && (transform.position.x > transform.parent.GetComponent<Collider2D>().bounds.max.x)) || ((direction == -1) && (transform.position.x < transform.parent.GetComponent<Collider2D>().bounds.min.x)))
        // Μπαίνει στο If όταν η θέση στον Χ του Enemy ειναι > από το δεξί άκρο του Parent Floor ή μικρότερη από το αριστερό αντίστοιχα.
        {
            direction *= -1;
            transform.Rotate(0, 180, 0); // Rotate για να κοιτάει στη σωστή κατεύθυνση
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision) // When the enemy touches the player :!!
    {
        Collider2D otherCollider = collision.collider;

        if (otherCollider == player.GetComponent<Collider2D>()) // Έλεγχος αν το otherCollider είναι το collider του player
        {
           anim.SetBool("Attack", true); // Eνεργοποιώ το animation attack
           StartCoroutine(Death());
        }
    }

    void OnCollisionExit2D(Collision2D collision) // When the enemy does not touches the player :!!
    {
        Collider2D otherCollider = collision.collider;
        anim.SetBool("Attack", false); // Disable attack animation..

       
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f); 
        playerRb.transform.Rotate(0, 0, 90);
        playerC.enabled = false; // Aπενεργοποιηση του collider του παίχτη ώστε να πέφτει
        yield return new WaitForSeconds(1f); // 1 sec αναμονή
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        DeathCount.Loss(); // Καλω την public συναρτηση για να αποθηκευω τα Losses
    }
}
