using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

	public int VidaMaxima = 200;
	public int VidaAtual;

	public GameObject deathEffect;
	public HealthBar barraVida;

	public void Start()
	{
		VidaAtual = VidaMaxima;
		barraVida.SetVidaMaxima(VidaMaxima);
	}
	public void TakeDamage(int damage)
	{
		VidaAtual -= damage;
		barraVida.SetVida(VidaAtual);

		StartCoroutine(DamageAnimation());

		if (VidaAtual <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	IEnumerator DamageAnimation()
	{
		SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 3; i++)
		{
			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 0;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in srs)
			{
				Color c = sr.color;
				c.a = 1;
				sr.color = c;
			}

			yield return new WaitForSeconds(.1f);
		}
	}

}
