using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class HitBoxAttack : PlayerAttackState
{
    protected HitBox _hitBox;

    private void Start()
    {
        _hitBox = GetComponentInChildren<HitBox>();

        _hitBox.TargetEntered += HitBoxEntered;
        _hitBox.TargetStayed += HitBoxStayed;
        _hitBox.TargetExited += HitBoxExited;
    }

    public override void Activate()
    {
        base.Activate();
        _hitBox.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
        _hitBox.Deactivate();
    }

    private void OnDestroy()
    {
        _hitBox.TargetEntered -= HitBoxEntered;
        _hitBox.TargetStayed -= HitBoxStayed;
        _hitBox.TargetExited -= HitBoxExited;
    }

    protected virtual void HitBoxEntered(ITarget target)
    {
        target.DebugIndicateHit(Color.red);
    }

    protected virtual void HitBoxExited(ITarget target)
    {
        ;
    }

    protected virtual void HitBoxStayed(ITarget target)
    {

    }

}

