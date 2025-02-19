using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sword : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)  // Αν συγκρούστηκε με player
        {
            StartCoroutine(death());
        }
    }

    IEnumerator death()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
