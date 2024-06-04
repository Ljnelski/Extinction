using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugInWorldUI : MonoBehaviour
{
    //[SerializeField] private TMP_Text _text;

    Vector3 cameraDir;

    public void Update()
    {
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    public void DrawMessage(string message)
    {
        //  _text.text = message;
    }
}
