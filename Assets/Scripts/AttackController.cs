using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Fighter fighter;
    public AudioSource whooshSource;
    public AttackData currentAttack;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // For each attack possible attack move, check if it was pressed
        foreach (AttackData attackData in fighter.FighterData.attackDatas)
        {
            if (Input.GetKey(attackData.inputKeyCode) && CanPlayAttack(attackData))
            {
                PlayAttack(attackData);
            }
            if (Input.GetKeyUp(attackData.inputKeyCode))
            {
                anim.SetBool(attackData.attackAnimation, false);
            }
        }
    }

    bool CanPlayAttack(AttackData attackData)
    {
        if (((fighter.fighterState == FighterState.Neutral || fighter.fighterState == FighterState.Walking) &&
                currentAttack == null) ||
            (fighter.fighterState == FighterState.Recovering && PreviousAttackIsChain(attackData)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Return if previous attack is able to chain into the new attack
    bool PreviousAttackIsChain(AttackData newAttack)
    {
        if (currentAttack != null)
        {
            foreach (var chainAttack in currentAttack.chainAttacks)
            {
                if (newAttack.name == chainAttack.name)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void PlayAttack(AttackData attackData)
    {
        whooshSource.Play();
        fighter.SetFighterState(FighterState.Attacking);
        currentAttack = attackData;
        anim.SetBool(attackData.attackAnimation, true);
        Debug.Log("Attack: " + currentAttack.name);
    }

    public void FinishAttack()
    {
        currentAttack = null;
        fighter.SetFighterState(FighterState.Neutral);
    }
}
