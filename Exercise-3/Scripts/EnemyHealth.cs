using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 100f;
    public TMP_Text health;
    public AudioSource dead;
    // Start is called before the first frame update
    public void reduceHealth(float value)
    {
        enemyHealth += value;
    }

    void Update()
    {
        health.text = $"Health: {enemyHealth}";

        if (enemyHealth == 0)
        {
            dead.Play(); // play sound
            Destroy(this.gameObject);
        }
    }
}
