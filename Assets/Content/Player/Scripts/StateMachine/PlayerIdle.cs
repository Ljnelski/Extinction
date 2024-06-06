
using UnityEngine;


public class PlayerIdle : PlayerState
{
    public override void Enter()
    {

    }

    public override void Run()
    {
        _player.Rotate();

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_player.BiteAttack.CanStart())
            {
                _player.SetState(_player.BiteAttack);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_player.ClawSwipeLeftAttack.CanStart())
            {
                _player.SetState(_player.ClawSwipeLeftAttack);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (_player.ClawSwipeRightAttack.CanStart())
            {
                _player.SetState(_player.ClawSwipeRightAttack);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (_player.WingFlapleftAttack.CanStart() && _player.WingFlapRightAttack.CanStart())
            {
                _player.SetState(_player.WingFlapleftAttack);
                _player.SetState(_player.WingFlapRightAttack);
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (_player.BreathAttack.CanStart())
            {
                Debug.Log("BreathingAttackInputConditionsMet");
                _player.SetState(_player.BreathAttack);
            }
        }

        if (_player.Stats.Health <= 0)
        {
            _player.SetState(_player.Dead);
        }
    }

    public override void Exit()
    {
    }
}
