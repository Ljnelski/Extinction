using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttack : HitBoxAttack
{
    [SerializeField] HitBox hitbox;

    private AttackDirection _attackDir;

    public void SetDirection(AttackDirection dir)
    {
        _attackDir = dir;
    }

    public override void Enter()
    {
        base.Enter();
        _hitBox = hitbox;
        if (_attackDir == AttackDirection.Left)
        {
            Animator.SetTrigger(_player.LeftSwipeTriggerID);
        }
        else if ( _attackDir == AttackDirection.Right)
        {
            Animator.SetTrigger(_player.RightSwipeTriggerID);
        }
    }
    public override void Run(PlayerInputRecorder playerInput)
    {
        base.Run(playerInput);

        if (_player.Stats.Health <= 0)
        {
            _player.SetState(_player.Dead);
        }
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

    public enum AttackDirection
    {
        Left,
        Right
    }
}
