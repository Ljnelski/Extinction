using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class StartGame : MonoBehaviour
{

    [SerializeField] GameObject director;

    public void StartOpeningCutscene()
    {
        director.SetActive(true);
    }
}
