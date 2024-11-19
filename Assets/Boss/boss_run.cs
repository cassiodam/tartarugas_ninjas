using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_run : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float DistancPAtacar = 3f;
    Transform player;
    Rigidbody2D rbody;
    Boss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player  = GameObject.FindGameObjectWithTag("Player").transform;
        rbody = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 alvo = new Vector2(player.position.x, rbody.position.y);
        Vector2 NovaPosicao = Vector2.MoveTowards(rbody.position, alvo, speed * Time.fixedDeltaTime);
        rbody.MovePosition(NovaPosicao);

        if(Vector2.Distance(player.position, rbody.position) <= DistancPAtacar)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //evitar bug de atacar quando longe do player
        animator.ResetTrigger("Attack");
    }


}
