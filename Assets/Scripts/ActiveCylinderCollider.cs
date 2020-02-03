using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCylinderCollider : MonoBehaviour
{
    public Fighter ownerFighter;
    public new string name;
    public float damage;
    public string hitAnimation; // name of animation trigger if this HitCollider connects as an attack
    public GameObject hitSpark;

    private new bool enabled = true;
    Transform newHitSpark;

    // Start is called before the first frame update
    void Start()
    {
        damage = 20f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        // Detect if another fighter's HitCollider was hit
        Fighter otherFighter = other.transform.root.gameObject.GetComponent<Fighter>();
        HurtSphereCollider otherHitCollider = other.gameObject.GetComponent<HurtSphereCollider>();
        if (enabled && otherHitCollider != null && OtherFighterCanBeHit(otherFighter))
        {
            // Generate hit spark
            // Will need to clean this up when the animation is over
            newHitSpark = Instantiate(hitSpark, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Transform>();
            newHitSpark.position = otherHitCollider.transform.position;

            // Give damage to other fighter
            otherFighter.TakeDamage(damage, hitAnimation);

            // Disable it after hitting one person. This can be upgraded so that more than one people can be hit by a move
            enabled = false;
        }
    }

    bool OtherFighterCanBeHit(Fighter otherFighter)
    {
        return otherFighter != null && !otherFighter.isHurting && otherFighter != ownerFighter && otherFighter.isAlive;
    }
}
