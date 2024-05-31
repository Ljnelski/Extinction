using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttackStats", menuName = "Extinction/Enemy/EnemyAttackStats")]
public class EnemyAttackStats : ScriptableObject
{
    [SerializeField] private float _damageToHealth;
    [SerializeField] private float _damageToBodyPart;

    public float DamageToHealth => _damageToHealth;
    public float DamageToBodyPart => _damageToBodyPart;
}
