using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCount : MonoBehaviour
{
    public static void Loss()
    {
        int playerLosses = PlayerPrefs.GetInt("PlayerLosses", 0);  // Ανάκτηση του αριθμού των αποτυχιών (προεπιλογή: 0)
        playerLosses++;  // Αυξάνει τον αριθμό των αποτυχιών
        PlayerPrefs.SetInt("PlayerLosses", playerLosses);  // Αποθήκευση του νέου αριθμού
        PlayerPrefs.Save();  // Αποθήκευση των δεδομένων
    }
}
