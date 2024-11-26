using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerController
{

    // Variáveis para ativar/desativar movimento para os lados
    public bool MovimentacaoSwitch = true; // Habilita ou desabilita o movimento para os lados
    Sons sons;

private void Awake()
{
    sons = GameObject.FindWithTag("Sons").GetComponent<Sons>();
}

    // Inicialização dos componentes do Swordman
    private void Start()
    {
        m_CapsulleCollider = this.transform.GetComponent<CapsuleCollider2D>(); // Obtém o componente CapsuleCollider2D
        m_Anim = this.transform.Find("model").GetComponent<Animator>(); // Obtém o componente Animator do modelo
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D
    }

    // Atualização por frame para verificar inputs e limitar a velocidade do Rigidbody
    private void Update()
    {
        checkInput(); // Verifica os inputs do jogador

        // Limita a magnitude da velocidade do Rigidbody
        if (m_rigidbody.velocity.magnitude > 30)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x - 0.1f, m_rigidbody.velocity.y - 0.1f);
        }
    }

    // Função para verificar os inputs do jogador
    public void checkInput()
    {
        // Verifica se a tecla "S" foi pressionada (sentar)
        if (Input.GetKeyDown(KeyCode.S))
        {
            IsSit = true; // Marca o estado como sentado
            m_Anim.Play("Sit"); // Toca a animação de sentar
        }
        else if (Input.GetKeyUp(KeyCode.S)) // Verifica se a tecla "S" foi liberada
        {
            m_Anim.Play("Idle"); // Toca a animação de ociosidade
            IsSit = false; // Marca que o jogador não está mais sentado
        }

        // Impede outras animações enquanto o jogador está sentado ou "morrendo"
        if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Sit") || m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentJumpCount < JumpCount)
                {
                    DownJump();

                }
            }
            return; // Interrompe a execução para evitar outras ações
        }

        m_MoveX = Input.GetAxis("Horizontal"); // Obtém o movimento horizontal do jogador

        GroundCheckUpdate(); // Atualiza a verificação de chão

        // Controla as animações de ataque e movimento
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (Input.GetKey(KeyCode.Mouse0)) // Verifica se o botão esquerdo do mouse está pressionado
            {
                m_Anim.Play("Attack"); // Toca a animação de ataque
            }
            else
            {
                if (m_MoveX == 0) // Se o jogador não estiver se movendo
                {
                    if (!OnceJumpRayCheck)
                        m_Anim.Play("Idle"); // Toca a animação de ociosidade
                }
                else
                {
                    m_Anim.Play("Run"); // Toca a animação de corrida
                }
            }
        }

        // Verifica se a tecla "1" foi pressionada para executar a animação de morte
        if (Input.GetKey(KeyCode.Alpha1))
        {
            m_Anim.Play("Die");
        }

        // Movimento do personagem
        if (MovimentacaoSwitch && Input.GetKey(KeyCode.D)) // Movendo-se para a direita
        {
            if (isGrounded) // Se está no chão
            {
                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    return;

                transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime); // Movimenta horizontalmente
            }
            else
            {
                transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0)); // Movimenta no ar
            }

            if (!Input.GetKey(KeyCode.A)) // Garante que o jogador não está se movendo para a esquerda
                Girar(false); // Virar para a direita
        }
        else if (MovimentacaoSwitch && Input.GetKey(KeyCode.A)) // Movendo-se para a esquerda
        {
            if (isGrounded) // Se está no chão
            {
                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    return;

                transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);
            }
            else
            {
                transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));
            }

            if (!Input.GetKey(KeyCode.D)) // Garante que o jogador não está se movendo para a direita
                Girar(true); // Virar para a esquerda
        }

        // Verifica se a tecla "Espaço" foi pressionada para pular
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                return;

            if (currentJumpCount < JumpCount) // Permite pular se o número de saltos for menor que o permitido
            {
                if (!IsSit) // Se não estiver sentado
                {
                    prefromJump(); // Executa o salto normal
                    sons.PlayES(sons.pulo);
                }
                else
                {
                    DownJump(); // Executa o "Down Jump"
                }
            }
        }
    }

    // Evento chamado ao aterrissar
    protected override void LandingEvent()
    {
        // Toca a animação de ociosidade se não estiver correndo ou atacando
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            m_Anim.Play("Idle");
    }

    // Detector de colisão com coletáveis
private void OnTriggerEnter2D(Collider2D colisao)
{
    if (colisao.CompareTag("Coletaveis"))
    {
        Destroy(colisao.gameObject);
        Pontuacao.instanciar.AtualizaPontos();
        Debug.Log("+1 coletável");

        // Verifica o tipo de coletável para tocar o som apropriado
        if (colisao.name.Contains("pizza"))
        {
            sons.PlaySomColetavel(sons.coletarPizza);
        }
        else if (colisao.name.Contains("star"))
        {
            sons.PlaySomColetavel(sons.coletarStar);
        }
        else
        {
            Debug.LogWarning("Coletável desconhecido. Nenhum som tocado.");
        }
    }

    // Verifica se o jogador colidiu com o objeto de tag "Chimney"
    if (colisao.tag == "Chimney")
    {
        // Acessa o script PlayerHealth e aplica o dano
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1000); // Aplica 1000 de dano ao jogador
        }
    }
}



    // Função para ativar ou desativar o movimento para os lados
    public void AtivarDesativarMovimentoLados(bool ativar)
    {
        MovimentacaoSwitch = ativar;
    }
}
