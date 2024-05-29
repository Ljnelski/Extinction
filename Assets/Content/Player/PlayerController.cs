using System;
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
    [SerializeField] private ClawAttack _clawSwipeLeftAttack;
    [SerializeField] private ClawAttack _clawSwipeRightAttack;
    [SerializeField] private BiteAttack _biteAttack;
    [SerializeField] private BreathAttack _breathAttack;
    [SerializeField] private WingFlapAttack _wingFlapleftAttack;
    [SerializeField] private WingFlapAttack _wingFlapRightAttack;
    [SerializeField] private RoarAttack _roarAttack;

    [Header("BodyParts")]
    [SerializeField] private BodyPart _head;
    [SerializeField] private BodyPart _armLeft;
    [SerializeField] private BodyPart _armRight;
    [SerializeField] private BodyPart _wingLeft;
    [SerializeField] private BodyPart _wingRight;

    [Header("animation")]
    [SerializeField] private Animator _animator;

    [Header("Camera")]
    [SerializeField] private Transform _cameraLookTarget;
    [SerializeField] private Transform _cameraLookPivot;
    [SerializeField] private float _cameraRotationSpeed;

    private PlayerInputRecorder _input;
    private ActionTimer _attackTimer;

    public Dictionary<PlayerAttackType, float> _attacks;

    private IdleAttack _idleAttack;
    private PlayerAttackState _currentAttack;

    public int BiteTriggerID { get; private set; }
    public int LeftSwipeTriggerID { get; private set; }
    public int RightSwipeTriggerID { get; private set; }
    public int BreathBoolID { get; private set; }
    public int WingFlapTriggerID { get; private set; }
    public int RoarAttackAnimationTransition { get; private set; }

    public PlayerStats Stats => _stats;
    public Animator Animator => _animator;

    public ClawAttack ClawSwipeLeftAttack { get => _clawSwipeLeftAttack; }
    public ClawAttack ClawSwipeRightAttack { get => _clawSwipeRightAttack; }
    public BiteAttack BiteAttack { get => _biteAttack; }
    public BreathAttack BreathAttack { get => _breathAttack; }
    public WingFlapAttack WingFlapleftAttack { get => _wingFlapleftAttack; }
    public WingFlapAttack WingFlapRightAttack { get => _wingFlapRightAttack; }
    public RoarAttack RoarAttack { get => _roarAttack; }

    private void Awake()
    {
        _attacks = new Dictionary<PlayerAttackType, float>
        {
            { PlayerAttackType.Swipe, 30f },
            { PlayerAttackType.Bite, 75f },
            { PlayerAttackType.WingFlap, 50f },
            { PlayerAttackType.Breath, 30f },
            { PlayerAttackType.Roar, 20f }
        };

        SetUpAttacks();

        _currentAttack = _idleAttack;

        LeftSwipeTriggerID = Animator.StringToHash("ClawSwipeLeft");
        RightSwipeTriggerID = Animator.StringToHash("ClawSwipeRight");
        BiteTriggerID = Animator.StringToHash("Bite");
        BreathBoolID = Animator.StringToHash("Breath");
        WingFlapTriggerID = Animator.StringToHash("WingFlap");

        _stats.RestoreStats();
    }

    private void OnEnable()
    {
        _input = GetComponent<PlayerInputRecorder>();
        _input.TestInputPressed += TestFunction;
    }

    private void OnDisable()
    {
        _input.TestInputPressed -= TestFunction;
    }

    private void SetUpAttacks()
    {
        _idleAttack = GetComponent<IdleAttack>();

        if (_idleAttack == null)
        {
            gameObject.AddComponent<IdleAttack>();
            _idleAttack = GetComponent<IdleAttack>();
        }

        _idleAttack.Init(this, null);

        string errorMsg = "";

        if (_clawSwipeLeftAttack == null)
        {
            errorMsg += "Left Claw Swipe, ";
        }
        if (_clawSwipeRightAttack == null)
        {
            errorMsg += "Right Claw Swipe , ";
        }
        if (_biteAttack == null)
        {
            errorMsg += "Bite, ";
        }
        if (_breathAttack == null)
        {
            errorMsg += "Breath, ";
        }
        if (_wingFlapleftAttack == null)
        {
            errorMsg += "Wing Flap Left, ";
        }
        if (_wingFlapRightAttack == null)
        {
            errorMsg += "Wing Flap Right, ";
        }
        if (_roarAttack == null)
        {
            errorMsg += "Roar";
        }

        if (!String.IsNullOrEmpty(errorMsg))
        {
            errorMsg += " Attack(s) Are not Assigned";
            Debug.Log(errorMsg);
            return;
        }

        _clawSwipeLeftAttack.Init(this, _armLeft);
        _clawSwipeRightAttack.Init(this, _armRight);
        _biteAttack.Init(this, _head);
        _breathAttack.Init(this, _head);
        _wingFlapleftAttack.Init(this, _wingLeft);
        _wingFlapRightAttack.Init(this, _wingRight);
        _roarAttack.Init(this, _head);
    }

    private void TestFunction()
    {
        _stats.Health -= 10f;
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

        _currentAttack.Run(_input);

        if (_currentAttack.ForceExit())
        {
            _currentAttack.Exit();
        }


        _stats.Stamina += _stats.StaminaRegenerationRate * Time.deltaTime;
    }

    public void ExitAttack()
    {
        _currentAttack = _idleAttack;
    }

    public void AdvanceAttackPhase()
    {       
        _currentAttack.AdvanceAttackPhase();
    }

    public void SetAttack(PlayerAttackState attackState)
    {
        _currentAttack = attackState;
        _currentAttack.Enter();
    }
}


