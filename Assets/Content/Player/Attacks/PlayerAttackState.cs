using UnityEngine;

public abstract class PlayerAttackState : MonoBehaviour
{
    [SerializeField] private PlayerAttackStats _stats;

    protected PlayerController _player;
    protected BodyPart _bodyPart;

    protected AttackPhase _attackPhase = AttackPhase.InActive;

    public float Damage => _stats.Damage;
    public float DamageBroken => _stats.DamageBroken;
    public float StaminaCost => _bodyPart.IsBroken ? _stats.StaminaCostBroken : _stats.StaminaCost;        
    public float StaminaCostBroken => _stats.StaminaCostBroken;
    public float AttackDuration => _stats.AttackDuration;

    protected PlayerStats Stats => _player.Stats;
    protected Animator Animator => _player.Animator;

    public AttackPhase Phase => _attackPhase;

    public void Init(PlayerController player, BodyPart bodyPart)
    {
        _player = player;
        _bodyPart = bodyPart;
    }

    public virtual bool CanStart()
    {
        return Stats.Stamina > StaminaCost;
    }

    public virtual void Enter()
    {
        Stats.Stamina = Stats.Stamina - StaminaCost;
        AdvanceAttackPhase();       
    }

    public virtual void Activate()
    {
        ;
    }

    public abstract void Run(PlayerInputRecorder input);

    public virtual void Deactivate()
    {
        ;
    }

    public virtual void Exit()
    {
        _player.ExitAttack();
    }

    public void AdvanceAttackPhase()
    {
        switch (_attackPhase)
        {
            case AttackPhase.InActive:
                _attackPhase = AttackPhase.WaitingForActivation;
                break;
            case AttackPhase.WaitingForActivation:
                _attackPhase = AttackPhase.Activated;
                Activate();
                break;
            case AttackPhase.Activated:
                _attackPhase = AttackPhase.Deactivated;
                Deactivate();
                break;
            case AttackPhase.Deactivated:
                _attackPhase = AttackPhase.InActive;
                Exit();
                break;
            default:
                break;
        }

        //Debug.Log("Attack Phase Changed: " + _attackPhase);
    }
}

public enum AttackPhase
{
    InActive,
    WaitingForActivation, // Animation Running, waiting for animation event to active damage
    Activated, // Phase where damage is being done,
    Deactivated, // Finishing Animation before being Brought back to InActive
}
