using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName ="Extinction/AttackStats")]
public class PlayerAttackStats : ScriptableObject
{
    public float Damage;
    public float DamageBroken;
    public float StaminaCost;
    public float StaminaCostBroken;
    public float AttackDuration;
}
