using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] ParticleSystem collideParticle;
    [SerializeField] float speed = 10f; // Speed of the projectile

    Transform target;

    public void Init(Transform target)
    {
        this.target = target;
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
            if (target.TryGetComponent<IDamageable>(out var damagable))
            {
                damagable.RemoveHp(damage);
            }

            if (collideParticle)
            {
                var particle = Instantiate(collideParticle, transform.position, Quaternion.identity);
                Destroy(particle.gameObject, 4f);
            }

            Destroy(gameObject);
        }
    }
}