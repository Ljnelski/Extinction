using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAreaButton : MonoBehaviour
{
    [SerializeField] AttackArea area;

    public void OnHover(bool value)
    {
        area.GetComponent<MeshRenderer>().enabled = value;
    }

    public void OnClick()
    {
        area.Run();
    }
}