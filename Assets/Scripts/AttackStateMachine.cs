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
        fighter.SetIsNeutral(true);
        Debug.Log("Animation ended: " + fighter.FighterData.name);
    }
}