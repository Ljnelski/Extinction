using System.Collections;
using System.Collections.Generic;
using TimerUtility;
using Unity.VisualScripting;
using UnityEngine;

public class DebugTarget : MonoBehaviour, IDamageAble
{
    private MeshRenderer _meshRender;
    private ActionTimer _colorResetTimer;

    private void OnEnable()
    {
        _meshRender = GetComponent<MeshRenderer>();
        _colorResetTimer = new ActionTimer();
        _colorResetTimer.AddCompleteCallback(ResetColour);
    }

    private void Update()
    {
        _colorResetTimer.Tick(Time.deltaTime);
    }

    public void DebugIndicateHit(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_BaseColor", color);
        _meshRender.SetPropertyBlock(props);
        _colorResetTimer.Start(1f);
    }

    public void ResetColour()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_BaseColor", Color.blue);
        _meshRender.SetPropertyBlock(props);
    }

    public void ApplyDamage(float amount)
    {
        Debug.Log("Damage taken: " + amount);
    }

    public void ApplyEffect()
    {
        Debug.Log("Applying Effect");
    }
}
