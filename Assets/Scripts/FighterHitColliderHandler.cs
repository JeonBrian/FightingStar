﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterHitColliderHandler : MonoBehaviour
{
    public bool hideColliders;
    public Fighter ownerFigher;
    public GameObject activeCylinder;
    public Transform leftHand;
    public Transform rightHand;
    public Transform leftFoot;
    public Transform rightFoot;

    private GameObject currentActiveCylinder;

    void ActivateHitCollider(string attackName)
    {
        DrawActiveCylinder(attackName);
    }

    void DeactivateHitCollider()
    {
        DestroyActiveCylinder();
    }

    void DrawActiveCylinder(string attackName)
    {
        // Fetch the attack from data
        AttackData attackData = Resources.Load<AttackData>("Frank/Attacks/" + attackName);
        if (attackData != null)
        {
            Debug.Log("Draw active");
            // Instantiate Active Cylinder
            currentActiveCylinder = Instantiate(activeCylinder, new Vector3(0, 0, 0), Quaternion.identity).gameObject;

            // Position it to the body part, then offset of your choosing
            Transform parent = this.GetType().GetField(attackData.activeCylinderData.parent).GetValue(this) as Transform;
            currentActiveCylinder.transform.localPosition = parent.position + attackData.activeCylinderData.offset;

            // Rotate cylinder and offset it
            Vector3 rotation = attackData.activeCylinderData.rotation;
            currentActiveCylinder.transform.rotation = parent.rotation;
            currentActiveCylinder.transform.Rotate(rotation.x, rotation.y, rotation.z);

            // Set the owner fighter
            currentActiveCylinder.GetComponent<ActiveCylinderCollider>().ownerFighter = ownerFigher;

            // Hit animation
            currentActiveCylinder.GetComponent<ActiveCylinderCollider>().hitAnimation = attackData.hitAnimation;

            // Decide to hide collider or not
            if (hideColliders)
            {
                currentActiveCylinder.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            Debug.Log("FighterHitColliderHandler: Attack '" + attackName + "' was not found!");
        }
    }

    void DestroyActiveCylinder()
    {
        Destroy(currentActiveCylinder);
    }
}
