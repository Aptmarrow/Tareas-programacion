using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    private GameManager gameManager;
    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // En Update solo leemos los botones del teclado
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // Sacamos el .normalized para que el movimiento diagonal sume las velocidades
        // tal como pasaba en el "The World's Hardest Game" original.
        moveDirection = new Vector2(inputX, inputY);
    }

    void FixedUpdate()
    {
        // Usar velocity es mucho más a prueba de fallos para que no atravieses paredes
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameManager.GameOver();
        }
        
        if (collision.CompareTag("Finish"))
        {
            gameManager.WinLevel();
        }
    }
}