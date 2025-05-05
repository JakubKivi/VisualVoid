using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 5, -15); 
    public float smoothTime = 0.3f;


    private Vector3 velocity = Vector3.zero;
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
