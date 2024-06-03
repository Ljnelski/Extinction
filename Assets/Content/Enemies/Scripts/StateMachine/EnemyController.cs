using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using UnityEngine.PlayerLoop;


// TODO Implement a slower tick cycle to save performance
// TODO Extract a "Typed" StateMachineController For using Generics
public abstract class StateMachineController : MonoBehaviour
{
    protected State _currentState;

    private void FixedUpdate()
    {

        _currentState.Run();
    }

    public virtual void ChangeState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        OnStateInit();
        _currentState.Enter();

        OnStateChanged();
    }

    protected virtual void OnStateInit()
    {
        ;
    }

    protected virtual void OnStateChanged()
    {
        ;
    }
}

public class EnemyController : StateMachineController, IDamageAble
{
    [SerializeField] protected EnemyStats _stats;
    [SerializeField] protected float _attackRadius;

    [SerializeField] private EnemyAttackStats _attackStats;
    [SerializeField] public DebugColorChanger _colorChanger;

    [HideInInspector] public ProjectileSpawner projectileSpawner;

    [Header("Debug")]
    [SerializeField] private DebugInWorldUI _debugUI;

    private GameObject _player;
    private Animator _animator;
    [SerializeField] private NavMeshAgent _navAgent;
    private HurtBox _hurtBox;

    private float _health = 10;
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

    [SerializeField] float _speed = 2f;

    int _speedBuff = 0;
    public int SpeedBuff
    {
        get => _speedBuff;
        set
        {
            _speedBuff = value;
            // maybe set some speedup particles
            OnSpeedUpdated();
        }
    }

    public float Speed
    {
        get
        {
            if (SpeedBuff > 0)
            {
                return _speed * 5f;
            }
            else if (SpeedBuff < 0)
            {
                return _speed * 0.2f;
            }
            else
            {
                return _speed;
            }
        }

        set
        {
            _speed = value;
            OnSpeedUpdated();
        }
    }

    [SerializeField] ProtectiveBubble bubbleVisuals;
    bool _bubbleBuff = false;
    public bool BubbleBuff
    {
        get => _bubbleBuff;
        set
        {
            _bubbleBuff = value;
            bubbleVisuals.gameObject.SetActive(value);
        }
    }


    protected HitBox _hitBox;
    public HitBox HitBox => _hitBox;

    public EnemyAttackStats AttackStats => _attackStats;
    public float DistanceToPlayer => Vector3.Distance(Player.transform.position, transform.position);

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        _hurtBox = GetComponentInChildren<HurtBox>();
        _hitBox = GetComponentInChildren<HitBox>();
        projectileSpawner = GetComponentInChildren<ProjectileSpawner>();

        SetDefaultState();
    }

    void OnSpeedUpdated()
    {
        NavAgent.speed = Speed;
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

    protected override void OnStateInit()
    {
        base.OnStateInit();
        if (_currentState is EnemyState<EnemyController> enemyState)
        {
            enemyState.Init(this);
        }
    }

    protected override void OnStateChanged()
    {
        base.OnStateChanged();
        if (_debugUI) _debugUI.DrawMessage("State: " + _currentState.GetType().Name);
    }

    public virtual void SetDefaultState()
    {
        GetComponent<EnemyStateInit>()?.SetDefaultState(this);
    }

    public void ApplyDamage(float amount)
    {
        TakeDamage(amount);
        _colorChanger?.TriggerColorChange(Color.red, 0.5f);
    }

    public void ApplyEffect()
    {
    }

    public void DebugIndicateHit(Color color)
    {
    }

    public void Restore()
    {
        _health = Stats.Health;
        _zeroHealth = false;
        _navAgent.isStopped = false;
    }

    public void TakeDamage(float damage)
    {
        if (damage < float.Epsilon) return;

        if (BubbleBuff)
        {
            BubbleBuff = false;
            return;
        }

        if (ZeroHealth) return;

        _health = Mathf.Max(0f, _health - damage);

        if (_health < float.Epsilon)
        {
            //Debug.Log("Enemy Took Damage");
            _zeroHealth = true;
            ChangeState(new Die());
        }
    }

    private void Start()
    {
        OnSpeedUpdated();
    }



    private void Update()
    {
        Debug.Log("current state: " + _currentState);
        Debug.Log("current position: " + transform.position);
        Debug.Log("current destination: " + NavAgent.destination);
    }
}
