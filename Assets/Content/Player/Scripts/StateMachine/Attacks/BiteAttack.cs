using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAttack : HitBoxAttack
{
    public override void Enter()
    {
        base.Enter();
        Animator.SetTrigger(_player.BiteTriggerID);
        _player.MovementLocked = true;
    }
    public override void Run(PlayerInputRecorder playerInput)
    {
        base.Run(playerInput);
    }

    public override void Exit()
    {
        base.Exit();
        _player.MovementLocked = false;
    }

    protected override void HitBoxEntered(HitBox.HurtBoxHitData target)
    {
        base.HitBoxEntered(target);
        target.Damagable.ApplyDamage(Damage);
    }

}
