using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickenIdleBehaviour : StateMachineBehaviour
{
    public int numberOfAnims;
    public float minIdleTime;
    public float maxIdleTime;
    public float minBoredTime;
    public float maxBoredTime;

    private bool isBored;
    private float idleTime;
    private float boredTime = 0f;
    private float timeUntilBored;
    private float timeUntilIdle;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        ResetIdle(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex
    )
    {
        if (!isBored)
        {
            idleTime += Time.deltaTime;

            if (idleTime > timeUntilBored)
            {
                isBored = true;
                float boredAnimation = Random.Range(1, numberOfAnims + 1);
                animator.SetFloat("BoredAnimation", boredAnimation);
                timeUntilIdle = Random.Range(minBoredTime, maxBoredTime);
            }
        }
        else
        {
            boredTime += Time.deltaTime;
            if (boredTime >= timeUntilIdle)
            {
                ResetIdle(animator);
                boredTime = 0f;
            }
        }
    }

    private void ResetIdle(Animator animator)
    {
        isBored = false;
        idleTime = 0;
        timeUntilIdle = Random.Range(minIdleTime, maxIdleTime);
        animator.SetFloat("BoredAnimation", 0);
    }
}
