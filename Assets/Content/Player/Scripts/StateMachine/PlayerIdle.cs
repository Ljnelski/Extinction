
using UnityEngine;


public class PlayerIdle : PlayerState
{
    public override void Enter()
    {

    }

    public override void Run(PlayerInputRecorder playerInput)
    {
        _player.Rotate();

        if (playerInput.Roar)
        {
            if (_player.BiteAttack.CanStart())
            {
                _player.SetState(_player.BiteAttack);
            }
        }
        else if (playerInput.PrimaryAttack)
        {
            if (_player.ClawSwipeLeftAttack.CanStart())
            {
                _player.SetState(_player.ClawSwipeLeftAttack);
            }
        }
        else if (playerInput.SecondaryAttack)
        {
            if (_player.ClawSwipeRightAttack.CanStart())
            {
                _player.SetState(_player.ClawSwipeRightAttack);
            }
        }
        else if (playerInput.WingAttack)
        {
            if (_player.WingFlapleftAttack.CanStart() && _player.WingFlapRightAttack.CanStart())
            {
                _player.SetState(_player.WingFlapleftAttack);
                _player.SetState(_player.WingFlapRightAttack);
            }
        }
        else if (playerInput.BreathAttack)
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
