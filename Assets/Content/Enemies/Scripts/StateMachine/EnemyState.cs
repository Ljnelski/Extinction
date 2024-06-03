using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State
{
    public abstract void Enter();
    public abstract void Run();
    public abstract void Exit();
}

public abstract class EnemyState<TController> : State where TController : EnemyController
{
    protected TController _controller;

    public virtual void Init(TController enemy)
    {
        _controller = enemy;
    }
}
