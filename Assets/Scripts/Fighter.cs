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
    public AudioSource hitSource;
    public AudioSource dieSource;

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
        SetIsNeutral(true);
        health = FighterData.health;
        isAttacking = false;
        isRecovering = false;
        isHurting = false;
        isAlive = true;
        SetRagdoll(false);
        Debug.Log("Loaded fighter..." + FighterData.description);
    }

    public void TakeDamage(float damage, string hitAnimation)
    {
        if (isAlive)
        {
            health -= damage;
            animator.SetTrigger(hitAnimation);
            hitSource.Play();

            if (health <= 0)
            {
                Die();
                Debug.Log(FighterData.name + " died!");
            }
        }
    }

    public void SetIsNeutral(bool isNeutral)
    {
        this.isNeutral = isNeutral;
        animator.SetBool("IsNeutral", isNeutral);
    }

    public void SetIsAttacking(bool isAttacking)
    {
        this.isAttacking = isAttacking;
        animator.SetBool("IsAttacking", isAttacking);
    }

    public void SetIsRecovering(bool isRecovering)
    {
        this.isRecovering = isRecovering;
        animator.SetBool("IsRecovering", isRecovering);
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
