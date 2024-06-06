using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    [SerializeField] private float _maxWidth;
    [SerializeField] private float _height;

    private RectTransform _rectTransform;

    private void OnValidate()
    {
        Valiate();
    }

    void Valiate()
    {
        if (_rectTransform == null)
        {
            _rectTransform = transform as RectTransform;
        }
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _maxWidth);
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _height);
    }

    private void Awake()
    {
        Valiate();
    }

    private void SetBarWidth(float width)
    {
        _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public void SetBarSize(float percentage)
    {
        //Debug.Log(percentage);
        SetBarWidth(_maxWidth * percentage);
    }

    public void SetBarSize(float currentValue, float maxValue)
    {
        //Debug.Log(currentValue + "/ " + maxValue);
        SetBarSize(currentValue / maxValue);
    }
}
