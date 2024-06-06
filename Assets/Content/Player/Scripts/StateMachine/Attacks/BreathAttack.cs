using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BreathAttack : HitBoxAttack
{
    [SerializeField] private float _staminaDrain;
    [SerializeField] private VisualEffect _fire;
    [SerializeField] HitBox hitbox;
    public override void Enter()
    {
        base.Enter();
        _hitBox = hitbox;

        Animator.SetBool(_player.BreathBoolID, true);
        //Debug.Log("Breath Attack Started!");
    }
    public override void Activate()
    {
        base.Activate();
        //Debug.Log("Breath Attack Activated");
        _fire.SendEvent("StartBreath");
    }

    public override void Run(PlayerInputRecorder playerInput)
    {
        base.Run(playerInput);

        _player.Rotate();

        if(_attackPhase == AttackPhase.Activated)
        {
            float staminaDrain = (_staminaDrain) * Time.deltaTime;

            if (!playerInput.BreathAttack || Stats.Stamina < staminaDrain)
            {
                //Debug.Log("breathing Canceled on AttackPhase: " + _attackPhase);
                Animator.SetBool(_player.BreathBoolID, false);
            }

            Stats.Stamina -= staminaDrain;
        }

        if (_player.Stats.Health <= 0)
        {
            _player.SetState(_player.Dead);
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _fire.SendEvent("StopBreath");
        //Debug.Log("Breath Attack Deactivated");
    }

    public override void Exit()
    {
        //Debug.Log("Breath Attack Ended!");
        base.Exit();
    }

    protected override void HitBoxStayed(HitBox.HurtBoxHitData target)
    {
        target.Damagable.DebugIndicateHit(Color.red);
        target.Damagable.ApplyDamage(Damage * Time.deltaTime);
    }

}
