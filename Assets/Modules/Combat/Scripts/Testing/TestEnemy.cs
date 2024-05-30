using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamagable
{
    [SerializeField] int hp = 10;

    public void RemoveHp(int value)
    {
        hp -= value;
        if (hp <= 0) Destroy(gameObject);
    }
}