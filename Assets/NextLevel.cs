using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Método que carrega a próxima cena
    public void NextLevelPoint()
    {
        // Carregar a próxima cena com base no índice da cena atual
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Verifica se o índice da próxima cena é válido
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Carregar a próxima cena
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Não há próxima cena disponível.");
        }
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (colisao.CompareTag("Player"))
        {
            // Chama o método para ir para a próxima fase
            NextLevelPoint();
        }
    }
}
