using UnityEngine;

[CreateAssetMenu()]
public class HurtSphereData : ScriptableObject
{
    public new string name;
    public string parent;
    public float radius;
    public Vector3 localPosition;
    public string hitAnimation;
}
