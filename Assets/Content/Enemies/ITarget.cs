using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface ITarget
{
    void DebugIndicateHit(Color color);

    public void ApplyDamage(float amount);
    public void ApplyEffect();
}


