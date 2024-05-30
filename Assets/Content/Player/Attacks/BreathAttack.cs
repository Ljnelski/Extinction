using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BreathAttack : HitBoxAttack
{
    [SerializeField] private float _staminaDrain;
    [SerializeField] private VisualEffect _fire;

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Breath Attack Started!");
    }
    public override void Activate()
    {
        base.Activate();
        _fire.SendEvent("StartBreath");
    }

    public override void Run(PlayerInputRecorder playerInput)
    {
        if(_attackPhase == AttackPhase.Activated)
        {
            float staminaDrain = (_staminaDrain + StaminaCost) * Time.deltaTime;

            if (!playerInput.BreathAttack || Stats.Stamina < staminaDrain)
            {
                Debug.Log("breathing Canceled");
                Animator.SetBool(_player.BreathBoolID, false);
            }

            Stats.Stamina -= staminaDrain;
        }        
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _fire.SendEvent("StopBreath");
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Breath Attack Ended!");

    }

    protected override void HitBoxStayed(ITarget target)
    {
        target.DebugIndicateHit(Color.red);
        target.ApplyDamage(Damage * Time.deltaTime);
    }

}
