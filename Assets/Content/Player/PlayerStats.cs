using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Extinction/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _staminaRegenerationRate;

    [SerializeField] private PlayerStatChanges _playerStatChanges;

    public float MaxHealth => _maxHealth;
    public float MaxStamina => _maxStamina;
    public float StaminaRegenerationRate => _staminaRegenerationRate;

    public float Health
    {
        get => _health;
        set
        {
            _health = Mathf.Max(0f, Mathf.Min(value, MaxHealth));
            _playerStatChanges.HealthChanged?.Invoke(_health / _maxHealth);
        }
    }

    public float Stamina
    {
        get => _stamina;
        set
        {
            _stamina = Mathf.Max(0f, Mathf.Min(value, MaxStamina));
            _playerStatChanges.StaminaChanged?.Invoke(_stamina / _maxStamina);
        }
    }


    private float _health;
    private float _stamina;

    public void RestoreStats()
    {
        _health = _maxHealth;
        _stamina = _maxStamina;
    }
}

[CreateAssetMenu(fileName = "PlayerStatChanges", menuName = "Extinction/PlayerStatChanges")]
public class PlayerStatChanges : ScriptableObject
{
    public Action<float> HealthChanged;
    public Action Died;
    public Action<float> StaminaChanged;
}
