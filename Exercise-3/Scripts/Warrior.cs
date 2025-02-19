using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Warrior : MonoBehaviour
{
    public UIforWoodland uiwood;
    public TMP_Text lost;
    public Transform player;
    public float difficulty = 4f;
    public Animator anim;
    public float detectionRange = 10f;
    public LayerMask detectionLayer; // Layers που μπορεί να ανιχνεύσει ο εχθρός

    void Start()
    {
        lost.text = "";
    }

    void Update()
    {
        // Υπολογίζουμε την κατεύθυνση από τον εχθρό προς τον παίκτη
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Ρίχνουμε το Raycast
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange, detectionLayer))
        {
                if (hit.collider.name.Equals("Player"))
                {
                    StartCoroutine(gotoplayer());
                }
        }

        // Οπτικοποίηση του Raycast 
        Debug.DrawRay(transform.position, directionToPlayer * detectionRange, Color.red);

        if (Managers.Player.health != null)
        {
            if (Managers.Player.health == 0)
            {
                StartCoroutine(die());
            }
        }
    }

private bool isChasing = false;
    IEnumerator gotoplayer()
    {
        if (isAttacking) yield break;
        isChasing = true;
        WanderingAI ai = GetComponent<WanderingAI>();
        ai.flag = true; // apenergopoiw apo to wanderingai script thn kinhsh
        transform.LookAt(player); // look at player
        anim.SetBool("run", true);
        transform.position = Vector3.MoveTowards(transform.position, player.position, difficulty * Time.deltaTime); // go to player 

        if (Vector3.Distance(player.position, transform.position) < 1f) // an o exthros ftasei ton paixth
        {
            StartCoroutine(attack());
        }
        isChasing = false;

        yield return null;
    }

private bool isAttacking = false;
    IEnumerator attack()
    {
        if (isAttacking) yield break; // Αν είναι ήδη σε επίθεση, δεν τρέχουμε πάλι
        isAttacking = true;
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(1.2f);
        if (!uiwood.defence)
        {
            Managers.Player.ChangeHealth(-5); // DAMAGE 10
        } 
        anim.SetBool("Attack", false);
        isAttacking = false; // Επιτρέπουμε την επόμενη επίθεση
    }

    public IEnumerator die()
    {
        lost.text = "Game Over";
        yield return new WaitForSeconds(0.8f); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
