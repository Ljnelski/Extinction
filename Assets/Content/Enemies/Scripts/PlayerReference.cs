using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerReference : MonoBehaviour
{
    private static PlayerReference _instance;

    public static PlayerReference Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<PlayerReference>();
                if(_instance == null)
                {
                    Debug.LogError("PlayerReference Not attached to player");
                }
            }
            
            return _instance;
        }
    }

    public GameObject Get()
    {
        return gameObject;
    }
}

