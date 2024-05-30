using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAttack : HitBoxAttack
{
    public override void Run(PlayerInputRecorder playerInput)
    {
        ;
    }    

    protected override void HitBoxEntered(ITarget target)
    {
        base.HitBoxEntered(target);
        target.ApplyDamage(Damage);
    }

}
