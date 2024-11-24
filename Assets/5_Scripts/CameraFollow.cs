using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform jogador; // Referência ao Transform do jogador
    public float suavizacao = 0.1f; // Fator de suavização da câmera
    private Vector3 posicaoOffset; // Deslocamento inicial da câmera

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
            // Calcula a nova posição da câmera, mantendo a altura fixa
            Vector3 novaPosicao = new Vector3(jogador.position.x + posicaoOffset.x, transform.position.y, transform.position.z);

            // Suaviza a movimentação da câmera
            transform.position = Vector3.Lerp(transform.position, novaPosicao, suavizacao);
        }
    }
}
