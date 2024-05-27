using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TimerUtility;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Locomotion")]
    [SerializeField] private Transform _playerPivot;
    [SerializeField] private float _rotationSpeed = 0.1f;

    [Header("Stats")]
    [SerializeField] private PlayerStats _stats;

    [Header("Attacks")]
    [SerializeField] private float _SwipeAttackSpeed;
    [SerializeField] private float _biteAttackSpeed;
    [SerializeField] private float _wingAttackSpeed;
    [SerializeField] private float _breathAttackSpeed;
    [SerializeField] private float _roarAttackSpeed;

    [Header("Camera")]
    [SerializeField] private Transform _cameraLookTarget;
    [SerializeField] private Transform _cameraLookPivot;
    [SerializeField] private float _cameraRotationSpeed;

    private PlayerInputRecorder _input;
    private ActionTimer _attackTimer;
    private ActionTimer _attackStartUpTimer;

    private bool _attacking = false;

    public Dictionary<PlayerAttackType, float> _attacks;

    private void Awake()
    {
        _attackTimer = new ActionTimer();
        _attackTimer.AddCompleteCallback(() => _attacking = false);

        _attackStartUpTimer = new ActionTimer();

        _attacks = new Dictionary<PlayerAttackType, float>
        {
            { PlayerAttackType.Swipe, 30f },
            { PlayerAttackType.Bite, 75f },
            { PlayerAttackType.WingFlap, 50f },
            { PlayerAttackType.Breath, 30f },
            { PlayerAttackType.Roar, 20f }
        };

        _stats.RestoreStats();
    }

    private void OnEnable()
    {
        _input = GetComponent<PlayerInputRecorder>();

        _input.TestInputPressed += TestFunction;
    }

    private void TestFunction()
    {
        _stats.Health -= 10f;
    }

    private void OnDisable()
    {
        _input.TestInputPressed -= TestFunction;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate The Camera Look Target as the player moves there mouse
        _cameraLookPivot.Rotate(new Vector3(0f, _input.LookDirection.x * _cameraRotationSpeed, 0f));

        // Rotate the player body to the direction the camera target is facing

        Vector3 lookDirection = Vector3.Slerp(_playerPivot.forward, _cameraLookPivot.forward, _rotationSpeed);

        Quaternion quaternion = Quaternion.LookRotation(lookDirection);

        _playerPivot.rotation = quaternion;

        if (!_attacking)
        {
            PollAttackInput();
        }
        else
        {
            _attackTimer.Tick(Time.deltaTime);
        }

        _stats.Stamina += _stats.StaminaRegenerationRate * Time.deltaTime;
    }

    private void Attack(PlayerAttackType attackType)
    {
        Debug.Log("Player Used: " +  attackType + ", cost: " + _attacks[attackType]);
        Debug.Log("Player has: " + _stats.Stamina + " stamina");
        _stats.Stamina -= _attacks[attackType];
        Debug.Log("Player has: " + _stats.Stamina + " stamina after attacking");

    }

    private void PollAttackInput()
    {
        if (_input.PrimaryAttack && _input.SecondaryAttack)
        {
            _attacking = true;
            _attackTimer.Start(_biteAttackSpeed);
            Attack(PlayerAttackType.Bite);
        }
        else if (_input.PrimaryAttack)
        {
            _attacking = true;
            _attackTimer.Start(_SwipeAttackSpeed);
            Attack(PlayerAttackType.Swipe);
        }
        else if (_input.SecondaryAttack)
        {
            _attacking = true;
            _attackTimer.Start(_SwipeAttackSpeed);
            Attack(PlayerAttackType.Swipe);
        }
        else if (_input.WingAttack)
        {
            _attacking = true;
            _attackTimer.Start(_wingAttackSpeed);
            Attack(PlayerAttackType.WingFlap);
        }
        else if (_input.BreathAttack)
        {
            _attacking = true;
            _attackTimer.Start(_breathAttackSpeed);
            Attack(PlayerAttackType.Breath);
        }
        else if (_input.Roar)
        {
            _attacking = true;
            _attackTimer.Start(_roarAttackSpeed);
            Attack(PlayerAttackType.Roar);
        }
    }

    private void DoLeftSwipe()
    {
        Debug.Log("Left Swipe");
    }

    private void DoRightSwipe()
    {
        Debug.Log("Right Swipe");
    }

    private void DoBiteAttack()
    {
        Debug.Log("Bite Attack");
    }

    private void DoWingFlap()
    {
        Debug.Log("Flap");
    }

    private void DoBreathAttack()
    {
        Debug.Log("Breath Attack");
    }

    private void DoTheRoar()
    {
        Debug.Log("RRRAAAAAAAWWWWWWWRRRRR!!!!");
    }
}


