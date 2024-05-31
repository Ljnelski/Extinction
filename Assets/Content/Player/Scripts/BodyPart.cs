using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour, IDamageAble, IBreakAble
{
    [SerializeField] private BodyPartType _partType;
    [SerializeField] private float _partHealth;

    public PlayerStats PlayerStats;

    public bool IsBroken => _isBroken;
    public BodyPartType PartType => _partType;

    private bool _isBroken = false;
    private float _health;

    public void DoBreakDamage(float damage)
    {
        _health = Mathf.Max(0, _health - damage);

        if(_health < 0)
        {
            _isBroken = true;
        }
    }

    public void DebugIndicateHit(Color color)
    {
        ;
    }

    public void ApplyDamage(float amount)
    {
        PlayerStats.Health -= amount;
    }

    public void ApplyEffect()
    {

    }
}

public enum BodyPartType
{
    Head,
    ArmLeft,
    ArmRight,
    WingLeft,
    WingRight    
}
