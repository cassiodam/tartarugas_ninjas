using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int attackDamage = 10; // Dano do ataque básico

    public Vector3 attackOffset; // Offset da posição de ataque
    public float attackRange = 1.5f; // Alcance do ataque
    public LayerMask attackMask; // Máscara para definir quais objetos podem ser atingidos

    // Método para o ataque básico
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            if (colInfo.TryGetComponent<BossHealth>(out BossHealth enemy))
            {
                enemy.TakeDamage(attackDamage); // Aplica dano no inimigo
            }
        }
    }

    // Método para visualizar o alcance do ataque no editor
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos, attackRange); // Desenha o alcance do ataque
    }
}
