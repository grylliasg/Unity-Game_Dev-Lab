using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    public float walkSpeed = 3.0f; // Ταχύτητα περπατήματος
    public float runSpeed = 6.0f; // Ταχύτητα τρεξίματος
    public float rotSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -20.0f;
    public float minFall = -1.5f;
    public float dashDistance = 5.0f; // Πόσο μακριά θα πάει το dash

    public float deceleration = 25.0f;
    public float targetBuffer = 1.5f;

    private float curSpeed = 0f;
    private Vector3? targetPos;
    private bool isRunning = false; // Κατάσταση τρεξίματος/περπατήματος

    private float vertSpeed;
    private ControllerColliderHit contact;

    private CharacterController charController;
    private Animator animator;

    public float pushForce = 3.0f;
    public AudioSource fall;
    private float dashSpeed = 50f;

    // Use this for initialization
    void Start()
    {
        vertSpeed = minFall;

        charController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFall(); // synexeia elegxos gia to an epese se gkremo 

        // Έλεγχος για Walk/Run toggle
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Πατημένο Shift για τρέξιμο
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // Απελευθέρωση Shift για περπάτημα
        {
            isRunning = false;
        }

        // Ρυθμίζει την τρέχουσα ταχύτητα ανάλογα με το αν τρέχει ή περπατά
        float moveSpeed = isRunning ? runSpeed : walkSpeed;

        // start with zero and add movement components progressively
        Vector3 movement = Vector3.zero;

        if (Input.GetMouseButton(0)) // Κλικ με το ποντίκι
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                targetPos = mouseHit.point;
                curSpeed = moveSpeed; // Ρύθμιση ταχύτητας με βάση την κατάσταση
            }
        }

        if (targetPos != null)
        {
            if (curSpeed > (moveSpeed * .5f))
            {
                Vector3 adjustedPos = new Vector3(targetPos.Value.x, transform.position.y, targetPos.Value.z);
                Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            }

            movement = curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);

            if (Vector3.Distance(targetPos.Value, transform.position) < targetBuffer)
            {
                curSpeed -= deceleration * Time.deltaTime;
                if (curSpeed <= 0)
                {
                    targetPos = null;
                }
            }
        }

        // Ρύθμιση animation με βάση την ταχύτητα
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetBool("Running", isRunning);

        // raycast down to address steep slopes and dropoff edge
        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (charController.height + charController.radius) / 1.9f;
            hitGround = hit.distance <= check; // to be sure check slightly beyond bottom of capsule
        }

        // y movement: possibly jump impulse up, always accel down
        if (hitGround)
        {
            vertSpeed = minFall;
            animator.SetBool("Jumping", false);
        }
        else
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
            if (contact != null)
            {   // not right at level start
                animator.SetBool("Jumping", true);
            }

            if (charController.isGrounded)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }
        movement.y = vertSpeed;

        movement *= Time.deltaTime;
        charController.Move(movement);

        if (Input.GetKeyDown(KeyCode.Z) && targetPos != null)
        {
            Vector3 adjustedPos = new Vector3(targetPos.Value.x, transform.position.y, targetPos.Value.z);
            Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
            transform.rotation = targetRot;

            StartCoroutine(Dash());
        }

    }

    // store collision to use in Update
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;

        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }

// αν πεφτει σε γκρεμο 
    void CheckFall()
    {
        // Έλεγχος αν η θέση του παίκτη στον άξονα y είναι μικρότερη από 5
        if (SceneManager.GetActiveScene().name == "Woodland" && transform.position.y < 5f)
        {
            Debug.Log("Game Over...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }     
    }

    IEnumerator Dash()
    {
        float dashTime = 0.04f; // Διάρκεια dash
        float elapsedTime = 0f;

        while (elapsedTime < dashTime)
        {
            charController.Move(transform.forward * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


}
