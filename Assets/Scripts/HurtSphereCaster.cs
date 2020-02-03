using UnityEngine;

public class HurtSphereCaster : MonoBehaviour
{
    public bool hideColliders;
    public GameObject hurtSphere;
    public Fighter fighter;
    public Transform leftHand;
    public Transform rightHand;
    public Transform head;
    public Transform chest;
    public Transform midSection;
    public Transform leftLeg;
    public Transform rightLeg;
    public Transform leftHip;
    public Transform rightHip;
    public Transform leftShoulder;
    public Transform rightShoulder;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateHurtSpheres();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Draw hit spheres for the character
    void InstantiateHurtSpheres()
    {
        foreach (var hurtSphereData in fighter.FighterData.hurtSphereDatas)
        {
            // Create HurtSphere
            HurtSphereCollider newCollider = Instantiate(hurtSphere, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<HurtSphereCollider>();

            // Set parents
            newCollider.ownerFighter = fighter;
            newCollider.transform.parent = this.GetType().GetField(hurtSphereData.parent).GetValue(this) as Transform;

            // Set position
            newCollider.transform.localPosition = hurtSphereData.localPosition;
            newCollider.name = hurtSphereData.name;

            // Set diameter
            float diameter = hurtSphereData.radius * 2;
            newCollider.transform.localScale = new Vector3(diameter, diameter, diameter);

            // Decide to hide collider or not
            if (hideColliders)
            {
                newCollider.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
