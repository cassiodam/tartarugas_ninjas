using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletion : MonoBehaviour
{
    private Timer timer; // Referência ao Timer.

    private void Start()
    {
        timer = FindObjectOfType<Timer>(); // Localiza o Timer no início.
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player") && SceneManager.GetActiveScene().name == "Level 2")
        {
            RegistrarMenorTempo();
            IrParaProximaCena();
        }
    }

    private void RegistrarMenorTempo()
    {
        float tempoAtual = timer != null ? timer.Duracao : float.MaxValue; // Acesse Duracao.
        float menorTempo = PlayerPrefs.GetFloat("Level2_MenorTempo", float.MaxValue);

        if (tempoAtual < menorTempo)
        {
            PlayerPrefs.SetFloat("Level2_MenorTempo", tempoAtual);
            Debug.Log($"Novo recorde no Level 2: {tempoAtual:F2} segundos!");
        }
    }

    private void IrParaProximaCena()
    {
        int proximaCena = SceneManager.GetActiveScene().buildIndex + 1;

        if (proximaCena < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(proximaCena);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
