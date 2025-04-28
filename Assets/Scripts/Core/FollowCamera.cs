using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -15);

    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
