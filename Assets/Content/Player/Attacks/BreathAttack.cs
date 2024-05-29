using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BreathAttack : HitBoxAttack
{
    [SerializeField] private VisualEffect _fire;

    public override void Activate()
    {
        base.Activate();
        _fire.SendEvent("StartBreath");
    }

    public override void Run(PlayerInputRecorder playerInput)
    {
        if (!playerInput.BreathAttack)
        {
            Animator.SetBool(_player.BreathBoolID, false);
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
    }

    public override bool ForceExit()
    {
        if (Stats.Stamina < StaminaCost)
        {
            return true;
        }

        return false;
    }

    protected override void HitBoxStayed(ITarget target)
    {
        target.DebugIndicateHit(Color.red);
        target.ApplyDamage(Damage * Time.deltaTime);
    }

}
