using System.Collections;
using System.Collections.Generic;
using TimerUtility;
using Unity.Properties;
using UnityEngine;

public class DebugColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private ActionTimer _timer;

    private Color _originalColor;

    private void OnEnable()
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        _originalColor = props.GetColor("_BaseColor");

        _timer = new ActionTimer();
        _timer.AddCompleteCallback(() => {
            ChangeColor(_originalColor);
        });
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }

    private void OnDisable()
    {
        ChangeColor(_originalColor);
    }

    public void TriggerColorChange(Color color, float time)
    {
        ChangeColor(color);
        _timer.Start(time);
    }

    private void ChangeColor(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_BaseColor", color);
        _meshRenderer.SetPropertyBlock(props);
    }


}
