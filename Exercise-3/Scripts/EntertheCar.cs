using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine.UI;

public class EntertheCar : MonoBehaviour
{
    private GameObject camera;
    public TMP_Text keys;
    public AudioSource car;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Use E to Enter the Car");
    }

    void OnTriggerStay(Collider other)
    {
        // Ενημέρωση του itemCount κάθε φορά που ο παίκτης είναι μέσα στην περιοχή του trigger
        int itemCount = Managers.Inventory.GetItemCount("CarKey");

        // Έλεγχος αν πατήθηκε το πλήκτρο E και οι συνθήκες για τα κλειδιά
        if (Input.GetKeyDown(KeyCode.E) && itemCount == 0)
        {
            Debug.LogWarning("You Need 2 More Keys");
        }
        else if (Input.GetKeyDown(KeyCode.E) && itemCount == 1)
        {
            Debug.LogWarning("You Need 1 More Key");
        }
        else if (Input.GetKeyDown(KeyCode.E) && itemCount >= 2)
        {
            ExitScene();
        }
    }

    void ExitScene()
        {
            // Βρίσκουμε την κύρια κάμερα
            GameObject camera = GameObject.Find("Main Camera");

            // Βρίσκουμε το Car (πρέπει να το έχεις ορίσει ως GameObject)
            GameObject car = GameObject.Find("Car");

            // Αν η κάμερα και το Car υπάρχουν
            if (camera != null && car != null)
            {
                // Αποκτούμε το OrbitCamera script και το απενεργοποιούμε
                OrbitCamera orbitCamera = camera.GetComponent<OrbitCamera>();
                if (orbitCamera != null)
                {
                    orbitCamera.enabled = false;
                }

                // Υπολογίζουμε τη νέα θέση της κάμερας πάνω από το Car
                Vector3 newCameraPosition = car.transform.position + new Vector3(0, 4, 4); // Θέση 10 μονάδες πάνω από το Car

                // Αλλάζουμε τη θέση της κάμερας
                camera.transform.position = newCameraPosition;

                // Αλλάζουμε την κατεύθυνση της κάμερας (προς το Car ή άλλο σημείο αν θέλεις)
                camera.transform.LookAt(car.transform.position); // Η κάμερα κοιτάει το Car

                // Κάνουμε την κάμερα child του Car
                camera.transform.parent = car.transform;
            }
            Rigidbody rb = car.GetComponent<Rigidbody>();
            float speed = 50f; // Ορίζουμε ταχύτητα
            rb.velocity = car.transform.up * speed * Time.deltaTime;

            GameObject player = GameObject.Find("player");
            Destroy(player);

            GameObject goal = GameObject.Find("goal");
            Destroy(goal);

            StartCoroutine(WaitAndExit());
 
        }

        void Update()
        {
            if (keys != null)
            {
                keys.text = Managers.Inventory.GetItemCount("CarKey").ToString();  // Ενημέρωση του UI Text
            }
        }


    IEnumerator WaitAndExit()
    {
        car.Play();
        Debug.Log("Level 2 Loading...");
        yield return new WaitForSeconds(3f); // Καθυστέρηση 2 δευτερολέπτων
        SceneManager.LoadScene("House");
    }

}
