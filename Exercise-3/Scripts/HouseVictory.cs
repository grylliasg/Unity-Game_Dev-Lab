using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseVictory : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject player;
    public GameObject enemy2;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Hit E to Enter the house");

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!enemy1 && !enemy2) // an exoun pethanei oloi oi enemies 
                {
                    SceneManager.LoadScene("WoodLand");
                }
                else
                {
                    Debug.Log("Kill all the enemies to Enter the House..");
                }
            }
        }
    }
}
