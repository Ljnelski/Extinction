using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatChanges", menuName = "Extinction/PlayerStatChanges")]
public class PlayerStatChanges : ScriptableObject
{
    public Action<float> HealthChanged;
    public Action Died;
    public Action<float> StaminaChanged;
}
