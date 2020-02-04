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
        SetRagdoll(false);
        Debug.Log("Loaded fighter..." + FighterData.description);
    }

    public void TakeDamage(float damage, string hitAnimation)
    {
        if (isAlive)
        {
            health -= damage;
            SetIsHitStun(true);
            hitSource.Play();

            if (health <= 0)
            {
                Die();
                Debug.Log(FighterData.name + " died!");
            }
        }
    }

    // Sets the fighterState on the fighter and the anim
    public void SetFighterState(FighterState fighterState)
    {
        this.fighterState = fighterState;
        animator.SetInteger("FighterState", fighterState.GetHashCode());
    }

    public void SetIsHitStun(bool isHitStun)
    {
        animator.SetBool("HitHighF", true);
        this.isHitStun = isHitStun;
        animator.SetBool("IsHitStun", isHitStun);
        // Replace
        // SetIsRecovering(false);
        // SetIsAttacking(false);
        // SetIsNeutral(false);
        Debug.Log("set is hitstun for " + gameObject.name + animator.GetBool("HitHighF"));
    }

    public void Die()
    {
        dieSource.Play();
        isNeutral = false;
        isAlive = false;
        // Blowback();
        animator.SetTrigger("HitKnockDown01");
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
