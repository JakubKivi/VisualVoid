using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public GameObject targetObject;  // Obiekt, do którego będzie aplikowana siła
    public float forceStrength = 10f;  // Siła, która będzie aplikowana
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Pobranie komponentu Rigidbody
    }

    void FixedUpdate()
    {
        if (targetObject != null)
        {
            // Obliczanie kierunku do obiektu
            Vector3 directionToTarget = (targetObject.transform.position - transform.position).normalized;

            // Aplikowanie siły w tym kierunku
            rb.AddForce(directionToTarget * forceStrength);
        }
    }
}


