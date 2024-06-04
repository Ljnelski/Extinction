using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private float _cameraRotationLimit = 85f;

    private PlayerInputRecorder _input;

    private PlayerIdle _idle;
    private PlayerDead _dead;
    private PlayerState _currentAttack;

    private Vector3 _cameraLookDirLimit;
    private float _rotateSpeed;

    private float _cameraAngle = 0;

    public bool MovementLocked { get; set; } = false;

    public int RotationFloatID { get; private set; }
    public int BiteTriggerID { get; private set; }
    public int LeftSwipeTriggerID { get; private set; }
    public int RightSwipeTriggerID { get; private set; }
    public int BreathBoolID { get; private set; }
    public int WingFlapTriggerID { get; private set; }
    public int RoarAttackAnimationTransition { get; private set; }
    public int DeathTriggerID { get; private set; }

    public PlayerStats Stats => _stats;
    public Animator Animator => _animator;

    public ClawAttack ClawSwipeLeftAttack { get => _clawSwipeLeftAttack; }
    public ClawAttack ClawSwipeRightAttack { get => _clawSwipeRightAttack; }
    public BiteAttack BiteAttack { get => _biteAttack; }
    public BreathAttack BreathAttack { get => _breathAttack; }
    public WingFlapAttack WingFlapleftAttack { get => _wingFlapleftAttack; }
    public WingFlapAttack WingFlapRightAttack { get => _wingFlapRightAttack; }
    public RoarAttack RoarAttack { get => _roarAttack; }
    public PlayerDead Dead { get => _dead; }

    private void OnEnable()
    {
        SetUpAttacks();

        _currentAttack = _idle;

        RotationFloatID = Animator.StringToHash("Rotation");
        LeftSwipeTriggerID = Animator.StringToHash("ClawSwipeLeft");
        RightSwipeTriggerID = Animator.StringToHash("ClawSwipeRight");
        BiteTriggerID = Animator.StringToHash("Bite");
        BreathBoolID = Animator.StringToHash("Breath");
        WingFlapTriggerID = Animator.StringToHash("WingFlap");
        DeathTriggerID = Animator.StringToHash("Death");

        _stats.RestoreStats();

        _head.PlayerStats = _stats;
        _armLeft.PlayerStats = _stats;
        _armRight.PlayerStats = _stats;
        _wingLeft.PlayerStats = _stats;
        _wingRight.PlayerStats = _stats;

        _input = GetComponent<PlayerInputRecorder>();
        _input.TestInputPressed += TestFunction;
    }

    private void OnDisable()
    {
        _input.TestInputPressed -= TestFunction;
    }

    private void SetUpAttacks()
    {
        _idle = GetComponent<PlayerIdle>();
        _dead = GetComponent<PlayerDead>();

        if (_idle == null)
        {
            gameObject.AddComponent<PlayerIdle>();
            _idle = GetComponent<PlayerIdle>();
        }
        if (_dead == null)
        {
            gameObject.AddComponent<PlayerDead>();
            _dead = gameObject.AddComponent<PlayerDead>();
        }

        _idle.Init(this);

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
        _clawSwipeLeftAttack.SetDirection(ClawAttack.AttackDirection.Left);

        _clawSwipeRightAttack.Init(this, _armRight);
        _clawSwipeRightAttack.SetDirection(ClawAttack.AttackDirection.Right);

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

        _currentAttack.Run(_input);
        _stats.Stamina += _stats.StaminaRegenerationRate * Time.deltaTime;

        if (Stats.Health <= 0)
        {
            _animator.SetBool("Death", true);

        }
    }

    public void Rotate()
    {
        if (!MovementLocked)
        {
            // Calculate the angle change
            float angle = _input.LookDirection.x * _cameraRotationSpeed;

            // Clamp angle change within limit
            _cameraAngle = Mathf.Min(Mathf.Max(_cameraAngle + angle, -_cameraRotationLimit), _cameraRotationLimit);

            // Turn the angle into a vector direction to look at
            Vector3 lookAtDir = Quaternion.AngleAxis(_cameraAngle, Vector3.up) * transform.forward;

            // Apply rotation transform
            _cameraLookPivot.LookAt(transform.position + lookAtDir);
        }

        // Rotate the player body to the direction the camera target is facing        
        Vector3 dirBeforeRotation = _playerPivot.forward;

        Vector3 lookDirection = Vector3.Slerp(_playerPivot.forward, _cameraLookPivot.forward, _rotationSpeed * Time.deltaTime);
        Quaternion quaternion = Quaternion.LookRotation(lookDirection);

        _playerPivot.rotation = quaternion;

        Vector3 dirAfterRotation = _playerPivot.forward;

        float rotationSpeed = Vector3.SignedAngle(dirBeforeRotation, dirAfterRotation, Vector3.up);

        //clamp rotation in the animator to turning left, turning right, or idle.


        Animator.SetFloat(RotationFloatID, rotationSpeed, 0.1f, Time.deltaTime);

    }

    public void ExitAttack()
    {
        _currentAttack = _idle;
    }

    public void AdvanceAttackPhase()
    {
        if (_currentAttack as PlayerAttackState)
        {
            ((PlayerAttackState)_currentAttack).QueuePhaseAdvance();
        }
    }

    public void SetState(PlayerState attackState)
    {
        _currentAttack = attackState;
        _currentAttack.Enter();
    }

    public void AttackedByEnemy(float damage, float breakDamage, Vector3 worldPos)
    {
        float[] distanceToBodyParts = new float[5];

        BodyPart[] bodyParts = new BodyPart[5];

        bodyParts[0] = _head;
        bodyParts[1] = _armLeft;
        bodyParts[2] = _armRight;
        bodyParts[3] = _wingLeft;
        bodyParts[4] = _wingRight;

        float distanceToHead = Vector3.Distance(_head.transform.position, worldPos);
        float distanceToArmLeft = Vector3.Distance(_armLeft.transform.position, worldPos);
        float distanceToArmRight = Vector3.Distance(_armRight.transform.position, worldPos);
        float distanceToWingLeft = Vector3.Distance(_wingLeft.transform.position, worldPos);
        float distanceToWingRight = Vector3.Distance(_wingRight.transform.position, worldPos);

        int bodyPartIndex = 0;

        if (distanceToHead > distanceToArmLeft)
        {
            bodyPartIndex = 1;
        }
        if (distanceToArmRight > distanceToArmLeft)
        {
            bodyPartIndex = 2;
        }
        if (distanceToArmRight > distanceToWingLeft)
        {
            bodyPartIndex = 3;
        }
        if (distanceToWingLeft > distanceToWingRight)
        {
            bodyPartIndex = 4;
        }

        bodyParts[bodyPartIndex].ApplyDamage(damage);
        bodyParts[bodyPartIndex].DoBreakDamage(breakDamage);

        Debug.Log("Doing Damage to: " + bodyPartIndex);
    }
}