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

    [Header("Body Parts")]
    [SerializeField] private BodyPart _head;
    [SerializeField] private BodyPart _armLeft;
    [SerializeField] private BodyPart _armRight;
    [SerializeField] private BodyPart _wingLeft;
    [SerializeField] private BodyPart _wingRight;

    [Header("Camera")]
    [SerializeField] private Transform _cameraLookTarget;
    [SerializeField] private Transform _cameraLookPivot;
    [SerializeField] private float _cameraRotationSpeed;

    private PlayerInputRecorder _input;
    private ActionTimer _attackTimer;
    private ActionTimer _attackStartUpTimer;

    private bool _attacking = false;

    public Dictionary<PlayerAttackType, float> _attacks;

    private PlayerAttack[] _playerAttacks;

    private ClawAttack _clawAttack;
    private BiteAttack _biteAttack;
    private BreathAttack _breathAttack;
    private WingFlapAttack _wingFlapAttack;
    private RoarAttack _roarAttack;


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

        _clawAttack = GetComponentInChildren<ClawAttack>();
        _biteAttack = GetComponentInChildren<BiteAttack>();
        _breathAttack = GetComponentInChildren<BreathAttack>();
        _wingFlapAttack = GetComponentInChildren<WingFlapAttack>();
        _roarAttack = GetComponentInChildren<RoarAttack>();

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

    private void Attack(PlayerAttack attack)
    {
        attack.ExecuteAttack(_stats);
        _attackTimer.Start(attack.AttackDuration);
    }

    private void PollAttackInput()
    {
        if (_input.PrimaryAttack && _input.SecondaryAttack)
        {
            _attacking = true;
            Attack(_biteAttack);
        }
        else if (_input.PrimaryAttack)
        {
            _attacking = true;
            Attack(_clawAttack);
        }
        else if (_input.SecondaryAttack)
        {
            _attacking = true;
            Attack(_clawAttack);
        }
        else if (_input.WingAttack)
        {
            _attacking = true;
            Attack(_wingFlapAttack);
        }
        else if (_input.BreathAttack)
        {
            _attacking = true;
            Attack(_breathAttack);
        }
        else if (_input.Roar)
        {
            _attacking = true;
            Attack(_roarAttack);
        }
    }
}


