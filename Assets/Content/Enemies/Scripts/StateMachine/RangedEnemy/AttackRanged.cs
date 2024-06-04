using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AttackRanged : StateWithTarget
{
    private float _attackCoolDownTimer;
    private float _attackDurationTimer;

    private bool _isAttacking;

    private float _tempAttackDuration = 1f;

    //private List<HitBox.HurtBoxHitData> _lastHits;

    bool _resetAfterOne;

    public AttackRanged(bool resetAfterOne = false)
    {
        _resetAfterOne = resetAfterOne;
    }

    public override void Init(EnemyController enemy)
    {
        base.Init(enemy);
        //_lastHits = new List<HitBox.HurtBoxHitData>();
    }

    public override void Enter()
    {
        _controller.Animator.SetBool("attacking", true);
    }

    public override void Run()
    {
        if (!_target || !IsTargetInRange())
        {
            _controller.SetDefaultState();
            return;
        }

        if (_isAttacking)
        {
            _attackDurationTimer += Time.fixedDeltaTime;
            if (_attackDurationTimer >= _tempAttackDuration)
            {
                _isAttacking = false;

                // Visual Effect of Attack
                _controller.projectileSpawner.Spawn(_target);

                // Actual Attack
                PlayerReference.Instance.GetPlayerController().AttackedByEnemy(
                    _controller.AttackStats.DamageToHealth,
                    _controller.AttackStats.DamageToBodyPart,
                    _controller.transform.position);


                if (_resetAfterOne)
                {
                    _controller.SetDefaultState();
                }
            }
        }
        else
        {
            _attackCoolDownTimer += Time.fixedDeltaTime;
            if (_attackCoolDownTimer >= _controller.Stats.AttackSpeed)
            {
                _attackCoolDownTimer = 0;
                _attackDurationTimer = 0;
                _isAttacking = true;
            }
        }
    }

    public override void Exit()
    {
        _attackCoolDownTimer = 0f;
        _attackCoolDownTimer = 0f;

        _isAttacking = false;

        _controller.Animator.SetBool("attacking", false);

    }


}
