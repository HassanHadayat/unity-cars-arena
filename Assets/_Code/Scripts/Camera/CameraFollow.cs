using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target GameObject to follow
    public Vector3 offset;
    public float followSpeed = 5f; // Speed of camera following
    public float rotationSpeed = 5f; // Speed of camera rotation

    void LateUpdate()
    {
        if (target != null)
        {
            FollowTarget();
            RotateWithTarget();
        }
    }

    void FollowTarget()
    {
        // Calculate the desired position behind the target
        Vector3 targetPosition = target.position + target.forward * offset.z + target.up * offset.y; // Adjust the values as needed

        transform.position = targetPosition;
    }

    void RotateWithTarget()
    {
        // Calculate the rotation that needs to be applied to face the target
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position, target.up);

        transform.rotation = targetRotation;
    }
}
