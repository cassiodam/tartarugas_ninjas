using UnityEngine;
using UnityEngine.SceneManagement;

public class Sons : MonoBehaviour
{
    [Header("----- Fonte dos Áudios -----")]
    [SerializeField] private AudioSource musicas;
    [SerializeField] private AudioSource efeitosSonoros;

    [Header("----- Lista de Áudios -----")]
    public AudioClip pulo;
    public AudioClip coletarPizza;
    public AudioClip coletarStar;

    [Header("----- Configuração de Sons de Fundo -----")]
    public AudioClip[] sonsDeFundoPorCena; // Sons de fundo para cada cena, configurados no Inspector

    private void Start()
    {
        // Determina o som de fundo baseado na cena atual
        int indiceCena = SceneManager.GetActiveScene().buildIndex;
        if (indiceCena < sonsDeFundoPorCena.Length && sonsDeFundoPorCena[indiceCena] != null)
        {
            musicas.clip = sonsDeFundoPorCena[indiceCena];
            musicas.loop = true; // Faz o som de fundo tocar em loop
            musicas.Play();
        }
        else
        {
            Debug.LogWarning("Nenhum som de fundo configurado para esta cena!");
        }
    }

    public void PlayES(AudioClip clip)
    {
        efeitosSonoros.PlayOneShot(clip);
    }

    public void PlaySomColetavel(AudioClip somColetavel)
    {
        if (somColetavel != null)
        {
            PlayES(somColetavel);
        }
        else
        {
            Debug.LogWarning("Nenhum som de coletável configurado!");
        }
    }
}
