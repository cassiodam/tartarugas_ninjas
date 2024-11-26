using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int attackDamage = 10; // Dano do ataque básico

    public Transform attackPoint; // Offset da posição de ataque
    public float attackRange = 1.5f; // Alcance do ataque
    public LayerMask attackMask; // Máscara para definir quais objetos podem ser atingidos


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }

    // Método para o ataque básico
	public void Attack()
	{
        //detectar inimigos no alcance do atk
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackMask);

        // causar dano
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<BossHealth>().TakeDamage(attackDamage);
            Debug.Log("Dano causado ao " + enemy.name);

        }
	}

    
    // Método para visualizar o alcance do ataque no editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange); // Desenha o alcance do ataque
    }


}