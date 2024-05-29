using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BreathAttack : PlayerAttackState
{
    [SerializeField] private VisualEffect _fire;

    public override void StartAttack()
    {
        base.StartAttack();
        Debug.Log("BreathAttack");
    }

    public override void Activate()
    {
        base.Activate();
        _fire.SendEvent("StartBreath");
    }

    public override void RunAttack(PlayerInputRecorder playerInput)
    {
        if (!playerInput.BreathAttack)
        {
            Animator.SetBool(_player.BreathBoolID, false);
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        Debug.Log("DeactivatingFire");
        _fire.SendEvent("StopBreath");
    }

    public override void ExitAttack()
    {
        base.ExitAttack();
    }

    public override bool ForceExit()
    {
        if (Stats.Stamina < StaminaCost)
        {
            return true;
        }

        return false;
    }
}
