using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackArea : MonoBehaviour
{
    [SerializeField] KeyCode key;

    void Update()
    {
        if (Input.GetKeyUp(key))
        {
            GetComponent<AttackArea>().Run();
        }
    }
}