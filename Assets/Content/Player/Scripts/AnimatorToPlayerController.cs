using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Need a way to call function in player controller from Animations so thats what this script does
public class AnimatorToPlayerController : MonoBehaviour
{
    [SerializeField] PlayerController _controller;

    public void AdvanceAttackPhase()
    {
        _controller.AdvanceAttackPhase();
    }
    
}
