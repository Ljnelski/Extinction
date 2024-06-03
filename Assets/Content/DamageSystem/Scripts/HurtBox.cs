using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour, IDamageAble
{
    public Action<float> DamageDealt;

    public void ApplyDamage(float amount)
    {
        DamageDealt?.Invoke(amount);
    }

    public void ApplyEffect()
    {
        throw new System.NotImplementedException();
    }

    public void DebugIndicateHit(Color color)
    {
        throw new System.NotImplementedException();
    }
}
