using UnityEngine;

[CreateAssetMenu()]
public class AttackData : ScriptableObject
{
    public new string name;
    public string description;
    public float damage;
    public KeyCode inputKeyCode;
    public string attackAnimation;
    public string hitAnimation;
    public ActiveCylinderData activeCylinderData;
    public AttackData[] chainAttacks;
}
