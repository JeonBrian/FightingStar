using System.IO;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Fighter : MonoBehaviour
{
    public FighterData FighterData;
    public float health; // Current health, derived from FighterData
    public bool isAttacking;
    public bool isRecovering;
    public bool isNeutral;
    public bool isHurting;
    public bool isAlive;
    public bool isHitStun;
    public AudioSource hitSource;
    public AudioSource dieSource;
    public int comboHitStun;
    public bool isFloating;

    public FighterState fighterState;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        LoadFighter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Instantiate fighter
    void LoadFighter()
    {
        SetFighterState(FighterState.Neutral);
        health = FighterData.health;
        isAlive = true;
        SetIsFloating(false);
        ResetComboHitStun();
        SetRagdoll(false);
        Debug.Log("Loaded fighter..." + FighterData.description);
    }

    public void TakeDamage(float damage, string hitAnimation)
    {
        if (isAlive)
        {
            ApplyDamage(damage, hitAnimation);

            if (health <= 0)
            {
                Die();
                Debug.Log(FighterData.name + " died!");
            }
        }
    }

    public void ApplyDamage(float damage, string hitAnimation)
    {
        health -= damage;
        IncrementComboHitStun();
        SetFighterState(FighterState.HitStun);

        // Interrupt whatever and play this animation
        if (isFloating == false)
        {
            animator.Play(hitAnimation, -1, 0f);
        }
        else
        {
            animator.Play("HitAir", -1, 0f);
        }
        SetFighterState(FighterState.HitStun);

        // If hitScrew, then set juggle state
        if (hitAnimation == "HitScrew" || hitAnimation == "HitKnockDown02" || hitAnimation == "HitSpin")
        {
            SetIsFloating(true);
        }

        // Play sound: move this
        hitSource.Play();
    }

    // ComboHitStun is used for keeping track of how many times the character was hit in a single sequence.
    // This is used to make sure combos are not ended prematurely.
    public void ResetComboHitStun()
    {
        comboHitStun = 0;
    }

    public void DecrementComboHitStun()
    {
        comboHitStun--;
    }

    void IncrementComboHitStun()
    {
        comboHitStun++;
    }

    // Sets the fighterState on the fighter and the anim
    public void SetFighterState(FighterState fighterState)
    {
        this.fighterState = fighterState;
        animator.SetInteger("FighterState", fighterState.GetHashCode());
    }

    public void SetIsFloating(bool isFloating)
    {
        this.isFloating = isFloating;
        animator.SetBool("IsFloating", isFloating);
    }

    public void Die()
    {
        dieSource.Play();
        isNeutral = false;
        isAlive = false;
        // Blowback();
        animator.Play("HitKnockDown01", -1, 0f);
        Invoke("Restart", 1.5f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SetRagdoll(bool enabled)
    {
        SetKinematic(!enabled);
        CapsuleCollider[] colliders = GetComponentsInChildren<CapsuleCollider>();
        foreach (CapsuleCollider cc in colliders)
        {
            cc.enabled = enabled;
        }
    }

    private void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }


    // Enable ragdoll and blow it back
    void Blowback()
    {
        // Switch to ragdoll
        gameObject.GetComponent<CharacterController>().enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        SetRagdoll(true);

        // Blow ragdoll back
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.AddForce(-transform.forward * 1000.0f);
        }
    }

}
