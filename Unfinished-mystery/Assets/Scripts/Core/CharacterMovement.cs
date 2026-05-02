using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 1.5f;
    public float runSpeed = 4f;
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    [Header("Camera")]
    public Transform cameraFollowTarget;

    [Header("Ground Detection")]
    public LayerMask groundMask;

    private CharacterController controller;
    private Animator animator;
    private float verticalVelocity;
    private bool isGrounded;

    private PlayerControls controls;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isRunning;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled  += ctx => moveInput = Vector2.zero;

        controls.Player.Jump.performed += ctx => jumpPressed = true;

        // If you add a "Run" action (e.g. L3 or R2), wire it here:
        // controls.Player.Run.performed += ctx => isRunning = true;
        // controls.Player.Run.canceled  += ctx => isRunning = false;
    }

    void OnEnable()  => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 2f, Vector3.down, out hit, 20f, groundMask))
        {
            float controllerBottom = controller.center.y - (controller.height / 2f);
            transform.position = new Vector3(
                transform.position.x,
                hit.point.y - controllerBottom,
                transform.position.z
            );
        }
    }

    void Update()
    {
        GroundCheck();
        HandleMovement();
        HandleJump();
        ApplyGravity();
        jumpPressed = false; // reset after processing
    }

    void GroundCheck()
    {
        float rayLength = (controller.height / 2f) + 0.15f;
        isGrounded = controller.isGrounded ||
                     Physics.Raycast(transform.position + controller.center, Vector3.down, rayLength, groundMask);
    }

    void HandleMovement()
    {
        float cameraYaw = cameraFollowTarget.eulerAngles.y;
        Quaternion cameraYawRotation = Quaternion.Euler(0, cameraYaw, 0);
        Vector3 moveDir = (cameraYawRotation * new Vector3(moveInput.x, 0, moveInput.y)).normalized;

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (moveDir.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
                rotationSpeed * Time.deltaTime);
            controller.Move(moveDir * currentSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed", moveDir.magnitude, 0.1f, Time.deltaTime);
    }

    void HandleJump()
    {
        if (jumpPressed && isGrounded)
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void ApplyGravity()
    {
        if (isGrounded && verticalVelocity < 0f)
            verticalVelocity = -5f;
        else
            verticalVelocity += gravity * Time.deltaTime;

        controller.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (controller == null) return;
        Gizmos.color = Color.red;
        float rayLength = (controller.height / 2f) + 0.15f;
        Gizmos.DrawRay(transform.position + controller.center, Vector3.down * rayLength);
    }
}