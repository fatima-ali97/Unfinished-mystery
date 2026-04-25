using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float distance = 3f;
    public float height = 4f;
    public float lookAtHeight = 2.2f;
    public float smoothSpeed = 8f;
    public float collisionOffset = 0.4f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 lookPoint = target.position + Vector3.up * lookAtHeight;

        Vector3 desiredPosition =
            target.position
            - target.forward * distance
            + Vector3.up * height;

        Vector3 direction = desiredPosition - lookPoint;

        if (Physics.Raycast(lookPoint, direction.normalized, out RaycastHit hit, direction.magnitude))
        {
            desiredPosition = hit.point + hit.normal * collisionOffset;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(lookPoint);
    }
}