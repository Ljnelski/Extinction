using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : HitBoxAttack
{
    public override void Enter()
    {
        base.Enter();

        if(_player.ClawSwipeLeftAttack)
        {
            Animator.SetTrigger(_player.LeftSwipeTriggerID);
        }
        else
        {
            Animator.SetTrigger(_player.RightSwipeTriggerID);
        }
    }
    public override void Run(PlayerInputRecorder playerInput)
    {
        base.Run(playerInput);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _hitBox.Deactivate();
    } 

    protected override void HitBoxEntered(HitBox.HurtBoxHitData target)
    {
        target.Damagable.ApplyDamage(Damage);
    }
}
