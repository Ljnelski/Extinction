using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool _showSpawnPointIndicator;    
    // Has a Mesh for showing the Spawn point/ moving it around for level design can be turn on and off

    private GameObject _spawnPointIndicator;
    
    private void OnValidate()
    {
        if(_spawnPointIndicator == null)
        {
            _spawnPointIndicator = transform.GetChild(0).gameObject;
        }   

        if(_showSpawnPointIndicator)
        {
            Show();
        } else
        {
            Hide();
        }
    }

    public void Show()
    {
        _spawnPointIndicator.SetActive(true);
    }
    public void Hide()
    {
        _spawnPointIndicator.SetActive(false);
    }

}
