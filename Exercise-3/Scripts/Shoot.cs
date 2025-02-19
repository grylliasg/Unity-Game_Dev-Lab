using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shotfx;
    public GameObject enemy;
    public Animator animator;
    public GameObject bullet;
    public EnemyHealth eh;
    // Start is called before the first frame update
    void Start()
    {
        eh = enemy.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            int itemCount = Managers.Inventory.GetItemCount("Gun");

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray apo camera se mouse

            if (Physics.Raycast(ray, out hit))
            {
                // Αν το αντικείμενο που χτυπήθηκε είναι ο εχθρός
                if (hit.collider.gameObject == enemy && itemCount != 0 && Vector3.Distance(enemy.transform.position, transform.position) < 8f)  
                {
                    StartCoroutine(shot());
                }
            }
        }
    }

    IEnumerator shot()
    {
        if (enemy != null)
        {
            transform.LookAt(enemy.transform);
            // Εκτόξευση του Prefab 
            GameObject spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            // Υπολογισμός κατεύθυνσης προς τον εχθρό
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            // Προσθήκη ταχύτητας στη σφαίρα
            Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();
            audioSource.PlayOneShot(shotfx); // play sound
            if (rb != null)
            {
                rb.velocity = direction * 25; // Εκτόξευση της σφαίρας
            }
             
            if (enemy != null)
            {
                animator.SetBool("hit", true);
                eh.reduceHealth(-20f);
            }

            yield return new WaitForSeconds(1.5f);

            // Επαναφορά του animation μόνο αν ο εχθρός υπάρχει ακόμα
            if (enemy != null)
            {
                animator.SetBool("hit", false);
            }
                }
            }
}
