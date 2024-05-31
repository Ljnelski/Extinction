using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Action<HurtBoxHitData> HurtBoxEntered;
    public Action<HurtBoxHitData> HurtBoxStayed;
    public Action<HurtBoxHitData> HurtBoxExited;

    private Collider _hurtBoxCollider;
    private void OnEnable()
    {
        _hurtBoxCollider = GetComponent<Collider>();

        if (_hurtBoxCollider == null)
        {
            Debug.LogError("No Collider On HurtBox");
        }

        if (!_hurtBoxCollider.isTrigger)
        {
            _hurtBoxCollider.isTrigger = true;
        }
    }

    public void Activate()
    {
        //Debug.Log("Activating Hitbox");
        _hurtBoxCollider.enabled = true;
    }

    public void Deactivate()
    {
        _hurtBoxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageAble damageAble = other.GetComponent<IDamageAble>();
        IBreakAble breakAble = other.GetComponent<IBreakAble>();

        if (damageAble != null || breakAble != null)
        {
            HurtBoxHitData hitData = new HurtBoxHitData(damageAble, breakAble);
            HurtBoxEntered?.Invoke(hitData);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        IDamageAble damageAble = other.GetComponent<IDamageAble>();
        IBreakAble breakAble = other.GetComponent<IBreakAble>();

        if (damageAble != null || breakAble != null)
        {
            HurtBoxHitData hitData = new HurtBoxHitData(damageAble, breakAble);
            HurtBoxStayed?.Invoke(hitData);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageAble damageAble = other.GetComponent<IDamageAble>();
        IBreakAble breakAble = other.GetComponent<IBreakAble>();

        if (damageAble != null || breakAble != null)
        {
            HurtBoxHitData hitData = new HurtBoxHitData(damageAble, breakAble);
            HurtBoxExited?.Invoke(hitData);
        }
    }
    public struct HurtBoxHitData
    {
        public HurtBoxHitData(IDamageAble damageAble, IBreakAble breakAble)
        {
            Damagable = damageAble;
            Breakable = breakAble;
        }

        public IDamageAble Damagable;
        public IBreakAble Breakable;
    }
}
