using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50f; // Ταχύτητα περιστροφής σε μοίρες ανά δευτερόλεπτο

    void Update()
    {
        // Περιστροφή γύρω από τον άξονα Y
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
