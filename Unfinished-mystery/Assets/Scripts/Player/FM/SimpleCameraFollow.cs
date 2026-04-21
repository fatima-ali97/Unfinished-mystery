using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 3f, -5f);
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // نخلي الكاميرا دايم ورا اللاعب
        Vector3 desiredPosition = target.position
                                + target.forward * offset.z
                                + Vector3.up * offset.y;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // تخلي الكاميرا تطالع اللاعب
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}