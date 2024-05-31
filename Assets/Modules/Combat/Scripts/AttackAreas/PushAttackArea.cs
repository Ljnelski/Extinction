using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAttackArea : AttackArea
{
    [SerializeField] float force = 100f;
    [SerializeField] Transform origin;

    public override void Attack(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 direction = (obj.transform.position - origin.position).normalized;
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}