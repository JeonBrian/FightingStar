using UnityEngine;

public class AttackStateMachine : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // When an attack animation ends, set the fighter back to neutral
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Fighter fighter = animator.GetComponent<Fighter>();
        AttackController attackController = animator.GetComponent<AttackController>();
        if (fighter.fighterState == FighterState.Recovering)
        {
            attackController.FinishAttack();
        }
    }
}