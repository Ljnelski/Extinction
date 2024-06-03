using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] Projectile prefabProjectile;

    public void Spawn(Transform target)
    {
        Instantiate(prefabProjectile, transform.position, Quaternion.identity)
            .Init(target);
    }
}