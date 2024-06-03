using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileAction
{
    void Apply(Transform target);
}