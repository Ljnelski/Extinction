using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;


// TODO Implement a slower tick cycle to save performance
// TODO Extract a "Typed" StateMachineController For using Generics
public abstract class StateMachineController : MonoBehaviour
{
    protected State _currentState;

    protected void SetFirstState(State firstState)
    {
        _currentState = firstState;
        _currentState.Enter();
        OnStateChanged();
    }

    private void Update()
    {
        _currentState.Run();
    }

    public virtual void ChangeState(State newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();

        OnStateChanged();
    }

    protected virtual void OnStateChanged()
    {
        ;
    }
}

public abstract class EnemyController : StateMachineController
{
    [SerializeField] protected EnemyStats _stats;
    [SerializeField] protected float _attackRadius;

    [Header("Debug")]
    [SerializeField] private DebugInWorldUI _debugUI;

    private GameObject _player;
    private Animator _animator;
    private NavMeshAgent _navAgent;
    private HurtBox _hurtBox;

    private float _health;
    private bool _zeroHealth;

    public EnemyStats Stats => _stats;
    public GameObject Player
    {
        get
        {
            if (_player == null)
            {
                _player = PlayerReference.Instance.Get();
            }

            return _player;
        }
    }

    public Animator Animator => _animator;
    public NavMeshAgent NavAgent => _navAgent;
    public HurtBox HurtBox => _hurtBox;
    public float AttackRadius => _attackRadius * 0.95f + _navAgent.radius;
    public bool ZeroHealth => _zeroHealth;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        _hurtBox = GetComponentInChildren<HurtBox>();

    }

    private void OnEnable()
    {
        _hurtBox.DamageDealt += TakeDamage;
        Restore();
    }

    private void OnDisable()
    {
        EnemyPool.Instance.EnqueueSoldier(_player);
        _hurtBox.DamageDealt -= TakeDamage;
    }

    protected override void OnStateChanged()
    {
        base.OnStateChanged();
        _debugUI.DrawMessage("State: " + _currentState.GetType().Name);
    }

    public void Restore()
    {
        _health = Stats.Health;
        _zeroHealth = false;
        _navAgent.isStopped = false;
    }

    public void TakeDamage(float damage)
    {
        _health = Mathf.Max(0f, _health - damage);

        if(_health < 0f)
        {
            Debug.Log("Enemy Took Damage");
            _zeroHealth = true;
        }
    }
}
