using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText; // Referência ao texto do Timer.

    private float duracao;
    public float tempoMaximo = 60f; // Tempo máximo para Level 1.

    // Propriedade pública para acessar a duração.
    public float Duracao => duracao;

    private void Update()
    {
        duracao += Time.deltaTime;

        int minutos = Mathf.FloorToInt(duracao / 60);
        int segundos = Mathf.FloorToInt(duracao % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutos, segundos);

        // Transição automática para a próxima cena se o tempo máximo for alcançado (apenas Level 1).
        if (SceneManager.GetActiveScene().name == "Level 1" && duracao >= tempoMaximo)
        {
            IrParaProximaCena();
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
            Debug.LogWarning("Não há mais cenas disponíveis.");
        }
    }
}
