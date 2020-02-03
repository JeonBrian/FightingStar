using UnityEngine;

[CreateAssetMenu()]
public class FighterData : ScriptableObject
{
    public new string name;
    public float health;
    public string description;
    public HurtSphereData[] hurtSphereDatas;
    public AttackData[] attackDatas;
}