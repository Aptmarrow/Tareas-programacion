using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Vector3 movementVector = new Vector3(0f, -5f, 0f);
    public float speed = 2f;
    public float startDelay = 0f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float distance = movementVector.magnitude;
        if (distance <= 0.001f) return;

        // PingPong ahora avanza a la velocidad exacta (unidades por segundo)
        float pingPongValue = Mathf.PingPong((Time.time + startDelay) * speed, distance);
        float t = pingPongValue / distance; // Lo convertimos a un valor de 0 a 1
        
        transform.position = startPosition + (movementVector * t);
    }
}