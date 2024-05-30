using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Action<ITarget> TargetEntered;
    public Action<ITarget> TargetStayed;
    public Action<ITarget> TargetExited;

    private Collider _hurtBoxCollider;
    private void OnEnable()
    {
        _hurtBoxCollider = GetComponent<Collider>();

        if(_hurtBoxCollider == null )
        {
            Debug.LogError("No Collider On HurtBox");
        }

        if(!_hurtBoxCollider.isTrigger)
        {
            _hurtBoxCollider.isTrigger = true;  
        }
    }

    public void Activate()
    {
        _hurtBoxCollider.enabled = true;
    }

    public void Deactivate()
    {
        _hurtBoxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        ITarget hurtBox = other.GetComponent<ITarget>();

        if (hurtBox != null)
        {
            Debug.Log(other.name + " entered hitbox");
            TargetEntered?.Invoke(hurtBox);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        ITarget hurtBox = other.GetComponent<ITarget>();
        if(hurtBox != null)
        {
            TargetStayed?.Invoke(hurtBox);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        ITarget hurtBox = other.GetComponent<ITarget>();

        if (hurtBox != null)
        {
            Debug.Log(other.name + " exited hurtbox");
            TargetExited?.Invoke(hurtBox);
        }
    }
}
