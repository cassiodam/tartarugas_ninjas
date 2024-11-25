using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform jogador; // Referência ao Transform do jogador
    public float suavizacao = 0.1f; // Fator de suavização da câmera
    private Vector3 posicaoOffset; // Deslocamento inicial da câmera

    [Header("Opções de Seguimento")]
    public bool seguirHorizontal = true; // Controla se a câmera segue na horizontal
    public bool seguirVertical = false; // Controla se a câmera segue na vertical

    void Start()
    {
        // Define o deslocamento inicial da câmera em relação ao jogador
        if (jogador != null)
        {
            posicaoOffset = transform.position - jogador.position;
        }
        else
        {
            Debug.LogError("Referência do jogador não atribuída no Start.");
        }
    }

    void LateUpdate()
    {
        if (jogador != null) // Verifica se a referência do jogador está atribuída
        {
            // Calcula a nova posição da câmera, respeitando os eixos configurados
            float novaPosX = seguirHorizontal ? jogador.position.x + posicaoOffset.x : transform.position.x;
            float novaPosY = seguirVertical ? jogador.position.y + posicaoOffset.y : transform.position.y;

            Vector3 novaPosicao = new Vector3(novaPosX, novaPosY, transform.position.z);

            // Suaviza a movimentação da câmera
            transform.position = Vector3.Lerp(transform.position, novaPosicao, suavizacao);
        }
    }
}
