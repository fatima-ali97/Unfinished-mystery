using UnityEngine;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class CharacterMovement : MonoBehaviour
{


    [Header("Movement")] public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    [Header("Camera")] public Transform cameraTransform;

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private bool isGrounded;

    // Animator parameter hashes (performance optimization)
    private int speedHash = Animator.StringToHash("Speed");
    private int isJumpingHash = Animator.StringToHash("IsJumping");

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move relative to camera direction
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = (camForward * v + camRight * h).normalized;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (moveDir.magnitude >= 0.1f)
        {
            // Rotate character to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            controller.Move(moveDir * currentSpeed * Time.deltaTime);
        }

        // Update animator
        float animSpeed = moveDir.magnitude * (isRunning ? 2f : 1f);
        animator.SetFloat(speedHash, animSpeed, 0.1f, Time.deltaTime);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool(isJumpingHash, true);
        }

        if (isGrounded && animator.GetBool(isJumpingHash))
            animator.SetBool(isJumpingHash, false);
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
