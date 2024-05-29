using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : HitBoxAttack
{
    public override void Run(PlayerInputRecorder playerInput)
    {
        ;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _hitBox.Deactivate();
    }

    public override bool ForceExit()
    {
        return false;
    }

    protected override void HitBoxEntered(ITarget target)
    {
        target.ApplyDamage(Damage);
    }
}
