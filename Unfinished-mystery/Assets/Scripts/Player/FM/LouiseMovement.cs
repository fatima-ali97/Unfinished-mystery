using UnityEngine;

public class LouiseMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float rotationSpeed = 150f;

    void Update()
    {
        float move = Input.GetAxis("Vertical");
        float turn = Input.GetAxis("Horizontal");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        transform.Rotate(0f, turn * rotationSpeed * Time.deltaTime, 0f);
        transform.Translate(0f, 0f, move * currentSpeed * Time.deltaTime);
    }
}