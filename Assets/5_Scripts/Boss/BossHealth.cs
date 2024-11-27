using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int VidaMaxima = 500;
    public int VidaAtual;

    public GameObject deathEffect;
    public bool isInvulnerable = false;
    public HealthBar barraVida;

    public GameObject mensagemFimDoJogo; // Arraste um GameObject de texto do Canvas no Inspector.

    private Timer timer;

    private void Start()
    {
        VidaAtual = VidaMaxima;
        barraVida.SetVidaMaxima(VidaMaxima);

        timer = FindObjectOfType<Timer>(); // Localiza o Timer no início.
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        VidaAtual -= damage;
        barraVida.SetVida(VidaAtual);

        if (VidaAtual <= 200)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (VidaAtual <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Registro do menor tempo ao derrotar o boss.
        RegistrarMenorTempoBoss();

        // Efeito de morte e destruição do boss (opcional).
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        // Inicia a corrotina para exibir a mensagem e voltar ao menu.
        StartCoroutine(ExibirMensagemFimDoJogo());
    }

    private void RegistrarMenorTempoBoss()
    {
        float tempoAtual = timer != null ? timer.Duracao : float.MaxValue; // Acesse Duracao.
        float menorTempo = PlayerPrefs.GetFloat("Level3_MenorTempo", float.MaxValue);

        if (tempoAtual < menorTempo)
        {
            PlayerPrefs.SetFloat("Level3_MenorTempo", tempoAtual);
            Debug.Log($"Novo recorde no Level 3 (Boss): {tempoAtual:F2} segundos!");
        }
    }

    private IEnumerator ExibirMensagemFimDoJogo()
    {
        // Exibe a mensagem de fim de jogo (ativa o texto no Canvas).
        if (mensagemFimDoJogo != null)
        {
            mensagemFimDoJogo.SetActive(true);
        }

        Debug.Log("Mensagem de fim de jogo exibida. Aguardando 10 segundos...");

        // Aguarda 10 segundos antes de voltar ao menu principal.
        yield return new WaitForSeconds(10f);

        Debug.Log("Tentando carregar a cena do menu principal (Scene 0)...");

 
     	SceneManager.LoadScene(0);
	}
    
  
}
