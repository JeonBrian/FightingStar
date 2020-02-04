using UnityEngine;

public class HitStunStateMachine : StateMachineBehaviour
{
    // When an attack animation ends, set the fighter back to neutral
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Fighter fighter = animator.GetComponent<Fighter>();
        AttackController attackController = animator.GetComponent<AttackController>();
        fighter.SetFighterState(FighterState.Neutral);
    }
}