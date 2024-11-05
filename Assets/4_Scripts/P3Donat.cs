using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Entrada de movimento
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;
    }

    private void FixedUpdate()
    {
        // Aplica a movimentação no Rigidbody do personagem
        rb.velocity = moveInput * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Detecta se o personagem está colidindo com um objeto "empurrável"
        if (collision.gameObject.CompareTag("Empurravel"))
        {
            Rigidbody2D blocoRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (blocoRb != null)
            {
                // Aplica uma força no bloco, na direção em que o personagem está se movendo
                blocoRb.AddForce(moveInput * speed, ForceMode2D.Force);
            }
        }
    }
}
