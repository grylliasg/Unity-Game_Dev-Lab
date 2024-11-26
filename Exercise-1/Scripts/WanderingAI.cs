using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float walk = 1.0f;
    public float obstacleRange = 5.0f;

    private bool isAlive;

    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;


    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            transform.Translate(0, 0, walk * Time.deltaTime);
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Debug.DrawRay(transform.position, transform.forward, Color.red);

        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab);
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }

    public void chasing(Vector3 vec)
    {
        //Debug.Log("Chasing!!");
        Vector3 direction = (vec - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}
