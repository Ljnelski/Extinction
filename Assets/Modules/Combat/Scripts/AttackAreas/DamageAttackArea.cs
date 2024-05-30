using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAttackArea : AttackArea
{
    [SerializeField] int damage;
    [SerializeField] ParticleSystem particlePrefab;


    public override void Attack(GameObject obj)
    {
        if (obj.TryGetComponent<IDamagable>(out var damagable))
        {
            damagable.RemoveHp(damage);
            if (particlePrefab)
            {
                var particle = Instantiate(particlePrefab, obj.transform.position, Quaternion.identity);
                Destroy(particle.gameObject, 4f);
            }
        }
        else
        {
            Destroy(obj);
            if (particlePrefab)
            {
                var particle = Instantiate(particlePrefab, obj.transform.position, Quaternion.identity);
                Destroy(particle.gameObject, 4f);
            }
        }
    }
}