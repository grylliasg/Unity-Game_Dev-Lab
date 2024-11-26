using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldOfView : MonoBehaviour
{
    public GameObject player;
    private WanderingAI wanderingai;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        wanderingai = GetComponentInParent<WanderingAI>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            Vector3 position = other.transform.position;
            wanderingai.chasing(position);
        }
    }
}
