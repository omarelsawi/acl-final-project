using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class post_attack : StateMachineBehaviour
{
    Transform link;
    link_main l_script;
    bool attacked;
    float attackRange;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        link = GameObject.FindGameObjectWithTag("Player").transform;
        l_script = GameObject.FindGameObjectWithTag("Player").GetComponent<link_main>();
        attacked = false;
        attackRange = 2.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!attacked && stateInfo.normalizedTime <= 0.6 && stateInfo.normalizedTime >= 0.4
            && Vector3.Distance(link.position, animator.transform.position) < attackRange)
        {
            attacked = true;

            if (stateInfo.IsName("Vertical Attack"))
                l_script.TakeDamage((animator.tag == "Moblin") ? 4 : 3);
            else
                l_script.TakeDamage((animator.tag == "Moblin") ? 2 : 1);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
