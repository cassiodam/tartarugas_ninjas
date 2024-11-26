using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

	public int VidaMaxima = 500;
	public int VidaAtual;

	public GameObject deathEffect;

	public bool isInvulnerable = false;
	public HealthBar barraVida;

	void Start()
	{
		VidaAtual = VidaMaxima;
		barraVida.SetVidaMaxima(VidaMaxima);
	}
	



	public void TakeDamage(int damage)
	{
		if (isInvulnerable)
			return;

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

	void Die()
	{
		//Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

}
