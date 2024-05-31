using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class HitBoxAttack : PlayerAttackState
{
    protected HitBox _hitBox;

    protected virtual void Start()
    {
        _hitBox = GetComponentInChildren<HitBox>();

        _hitBox.HurtBoxEntered += HitBoxEntered;
        _hitBox.HurtBoxStayed += HitBoxStayed;
        _hitBox.HurtBoxExited += HitBoxExited;
    }

    public override void Activate()
    {
        _hitBox.Activate();
    }

    public override void Deactivate()
    {
        _hitBox.Deactivate();
    }

    private void OnDestroy()
    {
        _hitBox.HurtBoxEntered -= HitBoxEntered;
        _hitBox.HurtBoxStayed -= HitBoxStayed;
        _hitBox.HurtBoxExited -= HitBoxExited;
    }

    protected virtual void HitBoxEntered(HitBox.HurtBoxHitData target)
    {
        target.Damagable?.DebugIndicateHit(Color.red);
    }

    protected virtual void HitBoxExited(HitBox.HurtBoxHitData target)
    {
        ;
    }

    protected virtual void HitBoxStayed(HitBox.HurtBoxHitData target)
    {

    }

}

