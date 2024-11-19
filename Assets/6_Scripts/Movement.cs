using UnityEngine;

public class Movement : MonoBehaviour
{
    public float velocidadeMovimento = 5f; // Velocidade de movimento
    public float forcaPulo = 5f;           // Força do pulo
    public float tempoMaximoPulo = 1.5f;   // Tempo máximo de pulo
    public Transform pontoDeRespawn;        // Referência ao ponto de respawn
    private float tempoPulo = 0f;           // Tempo atual de pulo
    private bool pulando = false;            // Verifica se o jogador está pulando
    private Rigidbody2D rb;                  // Componente Rigidbody2D do jogador
    private bool noChao;                     // Verifica se o jogador está no chão

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    // Obtém o componente Rigidbody2D
    }

    void Update()
    {
        Mover();
        Pular();
        VerificarAltura(); // Verifica a altura do jogador
    }

    void Mover()
    {
        float entradaMovimento = Input.GetAxis("Horizontal"); // Obtém a entrada horizontal
        rb.velocity = new Vector2(entradaMovimento * velocidadeMovimento, rb.velocity.y); // Move o jogador
    }

    void Pular()
    {
        if (noChao && Input.GetButtonDown("Jump")) // Verifica se o jogador está no chão e pressionou a tecla de pulo
        {
            pulando = true;
            tempoPulo = 0f; // Reseta o tempo de pulo
        }

        if (pulando)
        {
            tempoPulo += Time.deltaTime; // Incrementa o tempo de pulo

            if (tempoPulo < tempoMaximoPulo)
            {
                rb.velocity = new Vector2(rb.velocity.x, forcaPulo); // Aplica a força de pulo
            }

            if (Input.GetButtonUp("Jump"))
            {
                pulando = false; // Para o pulo se a tecla for solta
            }
        }
    }

    void VerificarAltura()
    {
        if (transform.position.y < -6f) // Verifica se a posição Y do jogador está abaixo de -6
        {
            Respawn(); // Chama o método de respawn
        }
    }

    void Respawn()
    {
        transform.position = pontoDeRespawn.position; // Respawna o jogador na posição do ponto de respawn
    }

    private void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao")) // Verifica se está colidindo com o chão
        {
            noChao = true; // O jogador está no chão
        }
    }

    private void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = false; // O jogador saiu do chão
        }
    }
}
