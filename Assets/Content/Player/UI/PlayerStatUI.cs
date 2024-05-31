using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    [SerializeField] private PlayerStatChanges _playerStatsChanges;
    
    [SerializeField] private StatusBar _healthbar;
    [SerializeField] private StatusBar _staminaBar;

    private void OnEnable()
    {
        _playerStatsChanges.HealthChanged += UpdateHealthBar;
        _playerStatsChanges.StaminaChanged += UpdateStaminaBar;
    }

    private void OnDisable()
    {
        _playerStatsChanges.HealthChanged -= UpdateHealthBar;
        _playerStatsChanges.StaminaChanged -= UpdateStaminaBar;
    }

    private void UpdateHealthBar(float value)
    {
        _healthbar.SetBarSize(value);
    }

    private void UpdateStaminaBar(float value)
    {
        _staminaBar.SetBarSize(value);
    }
}
