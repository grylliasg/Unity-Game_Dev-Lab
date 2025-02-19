using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheEnd : MonoBehaviour
{
    public TMP_Text win;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        win.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            win.enabled = true;
        }
    }
}
