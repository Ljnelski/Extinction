using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestPlayer : MonoBehaviour, IDamageable
{
    [SerializeField] TextMeshProUGUI txtHealth;
    [SerializeField] int startingHealth = 100;

    int hp;
    public int Health
    {
        get => hp;
        set
        {
            hp = value;
            UpdateHealthText();
        }
    }

    void Start()
    {
        Health = startingHealth;
    }

    public void RemoveHp(int value)
    {
        Health -= value;
        if (Health <= 0)
        {
            Health = 0;
            Debug.Log("Player is dead");
            Destroy(gameObject);
        }
    }

    void UpdateHealthText()
    {
        if (txtHealth != null)
        {
            txtHealth.text = $"Health: {hp}";
        }
    }
}