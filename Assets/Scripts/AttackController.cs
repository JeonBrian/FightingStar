using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public Fighter fighter;
    Animator anim;
    public AudioSource whooshSource;

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
            if (Input.GetKey(attackData.inputKeyCode))
            {
                whooshSource.Play();
                anim.SetBool("IsNeutral", false);
                anim.SetBool(attackData.attackAnimation, true);

            }
            if (Input.GetKeyUp(attackData.inputKeyCode))
            {
                anim.SetBool(attackData.attackAnimation, false);
            }
        }
    }
}
