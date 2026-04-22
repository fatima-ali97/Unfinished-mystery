using UnityEngine;

public class LouiseMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float rotationSpeed = 150f;

    private CharacterController controller;
    private float gravity = -9.81f;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Vertical");
        float turn = Input.GetAxisRaw("Horizontal");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Rotate player
        if (Mathf.Abs(turn) > 0.01f)
        {
            transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);
        }

        // Forward/backward movement
        Vector3 moveDirection = transform.forward * move * currentSpeed;

        // Gravity
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        controller.Move(moveDirection * Time.deltaTime);
    }
}