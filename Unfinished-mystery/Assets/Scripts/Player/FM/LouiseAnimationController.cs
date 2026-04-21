using UnityEngine;

public class LouiseAnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float speed = 0f;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isMoving = moveX != 0 || moveZ != 0;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isMoving)
            speed = 1f;

        if (isMoving && isRunning)
            speed = 2f;

        animator.SetFloat("Speed", speed);
    }
}