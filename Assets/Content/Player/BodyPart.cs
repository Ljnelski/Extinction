using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour //, IPlayerHurtBox
{
    [SerializeField] private BodyPartType _partType;
    [SerializeField] private float _partHealth;

    public bool IsBroken => _isBroken;
    public BodyPartType PartType => _partType;

    private bool _isBroken = false;
    private float _health;    
}

public enum BodyPartType
{
    Head,
    ArmLeft,
    ArmRight,
    WingLeft,
    WingRight    
}
