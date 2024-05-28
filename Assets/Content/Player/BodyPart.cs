using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    [SerializeField] private float _partHealth;

    public bool IsBroken => _isBroken;

    private bool _isBroken;
    private float _health;
}
