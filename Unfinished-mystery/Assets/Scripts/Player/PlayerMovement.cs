using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.8f;
    public float rotationSpeed = 10f;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z);

        bool isMoving = move.magnitude > 0.1f;

        if (animator != null)
        {
            animator.SetBool("isWalking", isMoving);
        }

        if (isMoving)
        {
            transform.forward = Vector3.Slerp(transform.forward, move.normalized, rotationSpeed * Time.deltaTime);
            controller.Move(move.normalized * speed * Time.deltaTime);
        }

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}