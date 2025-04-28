using UnityEngine;

public class RunnerController : MonoBehaviour
{
    public float speed = 7f;
    public float jumpHeight = 2.5f;
    public float gravity = 9.81f;
    public float stuckThreshold = 0.01f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float previousX;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        previousX = transform.position.x;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // Stały ruch w prawo
        Vector3 move = Vector3.right;
        controller.Move(move * speed * Time.deltaTime);

        // Sprawdzenie czy stoi w miejscu mimo ruchu
        float deltaX = transform.position.x - previousX;
        bool isStuck = Mathf.Abs(deltaX) < stuckThreshold;

        // Automatyczny skok jeśli utknął i jest na ziemi
        if (isStuck && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);

        // Grawitacja
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Zapisz pozycję X na następną klatkę
        previousX = transform.position.x;
    }
}
