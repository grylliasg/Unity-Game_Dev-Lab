using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooter : MonoBehaviour
{
    private GameObject player;
    private Collider2D playerC;
    private Rigidbody2D playerRb;
    private Vector2 movement;
    public float enemySpeed = .3f;
    private Animator anim;
    private float enemyX;
    private int direction = 1;
    public float detectRange = 6f;
    private Vector2 dir = Vector2.zero;
    private float lookY;
    
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

        lookY = Mathf.PingPong(Time.time * 0.2f, 0.3f) - 0.2f; // Αυξομείωση τιμης απο 0.1 μεχρι -0.1 για καλύτερη όραση του enemy 
        dir = new Vector2(direction, lookY).normalized; // Direction of the Raycast

        if (((direction == 1) && (transform.position.x > transform.parent.GetComponent<Collider2D>().bounds.max.x)) || ((direction == -1) && (transform.position.x < transform.parent.GetComponent<Collider2D>().bounds.min.x)))
        // Μπαίνει στο If όταν η θέση στον Χ του Enemy ειναι > από το δεξί άκρο του Parent Floor ή μικρότερη από το αριστερό αντίστοιχα.
        {
            direction *= -1;
            transform.Rotate(0, 180, 0); // Rotate για να κοιτάει στη σωστή κατεύθυνση
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, detectRange);

        if (hit.collider == playerC) // Έλεγχος αν το αντικείμενο που χτυπήθηκε είναι ο παίκτης
        {
            anim.SetBool("Fire", true); // Eνεργοποιώ το animation fire
            StartCoroutine(Death());
        }

        Debug.DrawRay(transform.position, dir * detectRange, Color.red); 
        
    }
    IEnumerator Death()
    {   
        yield return new WaitForSeconds(0.5f); 
        playerRb.transform.Rotate(0, 0, 90);
        playerC.enabled = false; // Aπενεργοποιηση του collider του παίχτη ώστε να πέφτει
        yield return new WaitForSeconds(1f); // Καθυστερεί για 1 sec
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        DeathCount.Loss();
    }
}
