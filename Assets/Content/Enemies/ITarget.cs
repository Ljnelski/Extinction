using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface ITarget
{
    void DebugIndicateHit(Color color);

    public void HitBySwipeAttack();
    public float HitByBiteAttack();
    public void HitByWingAttack();
    public void HitByBreathAttack();
    public void HitByRoar();
}


