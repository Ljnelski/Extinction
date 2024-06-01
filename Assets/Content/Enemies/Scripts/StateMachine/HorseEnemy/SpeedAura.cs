using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedAura : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<EnemyController>(out var enemy)) return;

        enemy.SpeedBuff++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<EnemyController>(out var enemy)) return;
        enemy.SpeedBuff--;
    }
}