using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public bool IsSit = false; // Indica se o jogador está sentado
    public int currentJumpCount = 0; // Contador do número atual de saltos realizados
    public bool isGrounded = false; // Indica se o jogador está no chão
    public bool OnceJumpRayCheck = false; // Indica se o jogador iniciou a verificação de salto usando o raycast

    public bool Is_DownJump_GroundCheck = false; // Indica se está realizando um "Down Jump" e verifica se há chão ou bloco abaixo
    protected float m_MoveX; // Valor do movimento horizontal do jogador
    public Rigidbody2D m_rigidbody; // Componente Rigidbody2D para controle de física do jogador
    protected CapsuleCollider2D m_CapsulleCollider; // Componente CapsuleCollider2D para detecção de colisões
    protected Animator m_Anim; // Componente Animator para controle de animações

    [Header("[Setting]")]
    public float MoveSpeed = 6; // Velocidade de movimento do jogador
    public int JumpCount = 2; // Número máximo de saltos permitidos
    public float jumpForce = 15f; // Força aplicada durante o pulo

  

    // Atualiza as animações do jogador com base no estado atual
    protected void AnimUpdate()
    {
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // Verifica se a animação atual não é "Attack"
        {
            if (Input.GetKey(KeyCode.Mouse0)) // Se o botão esquerdo do mouse estiver pressionado
            {
                m_Anim.Play("Attack"); // Toca a animação de ataque
            }
            else
            {
                if (m_MoveX == 0) // Se o jogador não está se movendo horizontalmente
                {
                    if (!OnceJumpRayCheck) // Se não estiver no meio de um salto
                        m_Anim.Play("Idle"); // Toca a animação de ociosidade
                }
                else
                {
                    m_Anim.Play("Run"); // Toca a animação de corrida
                }
            }
        }
    }
    
    // Altera a direção do jogador (espelhamento horizontal)
    protected void Girar(bool bLeft)
    {
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1); // Define o espelhamento no eixo X
    }

    // Realiza o pulo do jogador
    protected void prefromJump()
    {
        m_Anim.Play("Jump"); // Toca a animação de pulo

        m_rigidbody.velocity = new Vector2(0, 0); // Reseta a velocidade do jogador antes de aplicar a força do pulo

        m_rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // Aplica a força vertical para o pulo

        OnceJumpRayCheck = true; // Marca que o jogador está no ar
        isGrounded = false; // Indica que o jogador não está mais no chão
        currentJumpCount++; // Incrementa o contador de saltos
    }

    // Realiza o "Down Jump", descendo por plataformas
    protected void DownJump()
    {
        if (!isGrounded) // Se o jogador não estiver no chão, não permite o "Down Jump"
            return;

        if (!Is_DownJump_GroundCheck) // Se não houver chão diretamente abaixo do jogador
        {
            m_Anim.Play("Jump"); // Toca a animação de pulo
            m_rigidbody.AddForce(-Vector2.up * 10); // Aplica força para baixo
            isGrounded = false; // Marca que o jogador não está mais no chão
            m_CapsulleCollider.enabled = false; // Desativa o colisor do jogador para permitir a passagem por plataformas
            StartCoroutine(GroundCapsulleColliderTimmerFuc()); // Reativa o colisor após um curto intervalo
        }
    }

    // Temporizador para reativar o colisor após o "Down Jump"
    IEnumerator GroundCapsulleColliderTimmerFuc()
    {
        yield return new WaitForSeconds(0.3f); // Aguarda 0.3 segundos
        m_CapsulleCollider.enabled = true; // Reativa o colisor do jogador
    }

    // Verificação de chão utilizando Raycast
    Vector2 RayDir = Vector2.down; // Direção do raycast (para baixo)

    float PretmpY; // Variável para armazenar a posição Y anterior
    float GroundCheckUpdateTic = 0; // Contador de tempo para atualizações de verificação
    float GroundCheckUpdateTime = 0.01f; // Intervalo de tempo entre verificações

    // Verifica se o jogador está no chão durante o salto
    protected void GroundCheckUpdate()
    {
        if (!OnceJumpRayCheck) // Apenas executa a verificação se o jogador estiver no ar
            return;

        GroundCheckUpdateTic += Time.deltaTime; // Incrementa o contador de tempo

        if (GroundCheckUpdateTic > GroundCheckUpdateTime) // Se o intervalo de tempo for atingido
        {
            GroundCheckUpdateTic = 0; // Reseta o contador de tempo

            if (PretmpY == 0) // Se a posição Y anterior não foi inicializada
            {
                PretmpY = transform.position.y; // Armazena a posição Y atual
                return;
            }

            float reY = transform.position.y - PretmpY; // Calcula a diferença de altura desde a última verificação

            if (reY <= 0) // Se o jogador está descendo
            {
                if (isGrounded) // Se o jogador está no chão
                {
                    LandingEvent(); // Evento de aterrissagem
                    OnceJumpRayCheck = false; // Reseta a verificação de salto
                    Debug.Log("on the ground"); // Log informando que o jogador está no chão
                }
                else
                {
                    Debug.Log("not on the ground"); // Log informando que o jogador ainda não está no chão
                }
            }

            PretmpY = transform.position.y; // Atualiza a posição Y para a próxima verificação
        }
    }

    // Evento abstrato a ser implementado por classes derivadas para tratar a aterrissagem
    protected abstract void LandingEvent();



}
