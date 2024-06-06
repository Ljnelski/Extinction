using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public abstract class PlayerState : MonoBehaviour
{
    protected PlayerController _player;

    public virtual void Init(PlayerController player)
    {
        _player = player;
    }

    public abstract void Enter();

    public abstract void Run();

    public abstract void Exit();
   
}

