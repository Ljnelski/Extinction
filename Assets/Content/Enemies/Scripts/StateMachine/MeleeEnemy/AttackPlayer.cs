using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AttackPlayer : StateWithTarget
{
    private float _attackCoolDownTimer = 0;
    private float _attackDurationTimer;

    private bool _isAttacking;

    private float _tempAttackDuration = 1f;

    private List<HitBox.HurtBoxHitData> _lastHits;

    public override void Init(EnemyController enemy)
    {
        base.Init(enemy);
        _lastHits = new List<HitBox.HurtBoxHitData>();
    }

    public override void Enter()
    {
        _controller.HitBox.HurtBoxEntered += AddHitObject;
        
    }

    public override void Run()
    {
        _attackCoolDownTimer += Time.fixedDeltaTime;
        _attackDurationTimer += Time.fixedDeltaTime;
        
        if (_controller.DistanceToPlayer > _controller.AttackRadius)
        {
            _controller.SetDefaultState();
            _controller.animator.SetBool("attacking", false);
            _isAttacking = false;
        }
        
        else if (_attackCoolDownTimer >= _controller.Stats.AttackSpeed)
        {
            Debug.Log(_controller.name + " is attacking");
            _controller.animator.SetBool("attacking", true);
            _attackCoolDownTimer = 0;
            _controller.HitBox.Activate();
                
            // hard Code the Attack the the player
            PlayerReference.Instance.GetPlayerController().AttackedByEnemy(
                _controller.AttackStats.DamageToHealth,
                _controller.AttackStats.DamageToBodyPart,
                _controller.transform.position);

            _isAttacking = true;
        }
        else
        {
            _controller.animator.SetBool("attacking", false);
            ;
        }
        
    }

    public override void Exit()
    {
        _attackCoolDownTimer = 0f;
        _attackCoolDownTimer = 0f;
    
        _isAttacking = false;
        _controller.HitBox.Deactivate();
        _controller.HitBox.HurtBoxEntered -= AddHitObject;
    }

    private void AddHitObject(HitBox.HurtBoxHitData hitData)
    {
        _lastHits.Add(hitData);
    }


}
