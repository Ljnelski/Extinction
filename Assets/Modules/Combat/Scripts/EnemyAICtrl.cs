using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAICtrl : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public Projectile projectile;

    Transform target;
    Rigidbody rb;

    System.Action state;
    float lastAttackTime;
    float resetVelocityInterval = 2f;
    float lastResetTime;

    void Start()
    {
        target = CombatCtrl.Instance.player;
        rb = GetComponent<Rigidbody>();

        if (target) state = StateWalkTowardsTarget;
        lastResetTime = Time.time;
    }

    private void FixedUpdate()
    {
        state?.Invoke();

        // Reset Rigidbody velocity every resetVelocityInterval seconds
        if (Time.time - lastResetTime >= resetVelocityInterval)
        {
            rb.linearVelocity = Vector3.zero;
            lastResetTime = Time.time;
        }
    }

    // --------- States -------------
    void StateWalkTowardsTarget()
    {
        if (!target)
        {
            state = null;
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        // Check if the enemy is within attack range
        if (Vector3.Distance(transform.position, target.position) <= attackRange)
        {
            state = StateAttackTarget;
        }
    }

    void StateAttackTarget()
    {
        if (!target)
        {
            state = null;
            return;
        }

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            var newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.Init(target);

            lastAttackTime = Time.time;
        }

        // In case we get pushed away or something
        if (Vector3.Distance(transform.position, target.position) > attackRange)
        {
            state = StateWalkTowardsTarget;
        }
    }
}