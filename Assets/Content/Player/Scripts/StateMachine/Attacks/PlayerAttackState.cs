using UnityEngine;

public abstract class PlayerAttackState : PlayerState
{
    [SerializeField] private PlayerAttackStats _stats;

    protected BodyPart _bodyPart;
    protected PlayerStats Stats => _player.Stats;
    protected Animator Animator => _player.Animator;

    protected AttackPhase _attackPhase = AttackPhase.InActive;
    protected bool _advanceAttackPhase;

    public AttackPhase Phase => _attackPhase;
    public float Damage => _stats.Damage;
    public float DamageBroken => _stats.DamageBroken;
    public float StaminaCost => _bodyPart.IsBroken ? _stats.StaminaCostBroken : _stats.StaminaCost;        
    public float StaminaCostBroken => _stats.StaminaCostBroken;
    public float AttackDuration => _stats.AttackDuration;
    
    
    public void Init(PlayerController player, BodyPart bodyPart)
    {
        base.Init(player);
        _bodyPart = bodyPart;
    }

    public virtual bool CanStart()
    {
        return Stats.Stamina > StaminaCost;
    }

    public override void Enter()
    {
        Stats.Stamina = Stats.Stamina - StaminaCost;
        QueuePhaseAdvance();       
    }

    public abstract void Activate();

    public override void Run(PlayerInputRecorder input)
    {
        if(_advanceAttackPhase)
        {
            AdvanceAttackPhase();
        }
    }

    public abstract void Deactivate();

    public override void Exit()
    {
        _player.ExitAttack();
    }

    public void QueuePhaseAdvance()
    {
        _advanceAttackPhase = true;
    }
    protected void AdvanceAttackPhase()
    {
        _advanceAttackPhase = false;

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
    }
}

public enum AttackPhase
{
    InActive,
    WaitingForActivation, // Animation Running, waiting for animation event to active damage
    Activated, // Phase where damage is being done,
    Deactivated, // Finishing Animation before being Brought back to InActive
}
