using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    private CharacterController controller;
    public float gravity = -9.81f;
    public float jumpSpeed = 10.0f;
    Vector3 moveVelocity;
    private bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            doubleJump = true;
            moveVelocity.y = jumpSpeed;
        }

        if (!controller.isGrounded && Input.GetButtonDown("Jump") && doubleJump)
        {
            moveVelocity.y = jumpSpeed;
            doubleJump = false;
        }
        
        // Προσθήκη βαρύτητας
        moveVelocity.y += gravity * Time.deltaTime;
        controller.Move(moveVelocity * Time.deltaTime);
    }
}
