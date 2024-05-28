using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteAttack : PlayerAttack
{
    public override void ExecuteAttack(PlayerStats stats)
    {
        Debug.Log("Bite Attack");
    }
}
