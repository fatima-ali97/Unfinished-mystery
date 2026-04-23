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

        float move = Input.GetAxisRaw("Vertical");
        bool isMoving = Mathf.Abs(move) > 0.01f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isMoving)
            speed = 1f;

        if (isMoving && isRunning)
            speed = 2f;

        animator.SetFloat("Speed", speed);

        Debug.Log("Speed = " + speed);
    }
}