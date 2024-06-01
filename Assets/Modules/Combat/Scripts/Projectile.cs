using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    [SerializeField] float breakAmount = 2f;

    [SerializeField] ParticleSystem collideParticle;
    [SerializeField] float speed = 10f; // Speed of the projectile

    Transform target;

    System.Action<Transform> action; 

    public void Init(Transform target, System.Action<Transform> action = null)
    {
        this.target = target;
        this.action = action;
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);

        if (transform.position == target.position)
        {
            if (target.TryGetComponent<IDamageAble>(out var damagable))
            {
                damagable.ApplyDamage(damage);
            }

            if (target.TryGetComponent<IBreakAble>(out var breakable))
            {
                breakable.DoBreakDamage(breakAmount);
            }

            if (TryGetComponent<IProjectileAction>(out var projAction))
            {
                projAction.Apply(target);
            }

            action?.Invoke(target);

            if (collideParticle)
            {
                var particle = Instantiate(collideParticle, transform.position, Quaternion.identity);
                Destroy(particle.gameObject, 4f);
            }

            Destroy(gameObject);
        }
    }
}