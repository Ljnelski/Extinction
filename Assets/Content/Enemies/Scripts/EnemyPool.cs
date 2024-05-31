using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool _instance;

    [SerializeField] private GameObject _soldierPrefab;
    [SerializeField] private GameObject _knightPrefab;
    [SerializeField] private GameObject _championPrefab;

    public static EnemyPool Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<EnemyPool>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<EnemyPool>();
                }
            }

            return _instance;
        }
    }

    Queue<GameObject> _avaliableSoldiers = new Queue<GameObject>();
    Queue<GameObject> _avaliableKnights = new Queue<GameObject>();
    Queue<GameObject> _avaliableChampions = new Queue<GameObject>();

    private void Awake()
    {
        if( _instance != null)
        {
            Destroy(this);
        }
    }

    public void EnqueueSoldier(GameObject soilder)
    {        
        _avaliableSoldiers.Enqueue(soilder);
    }

    public void EnqueueKnight(GameObject knight)
    {
        _avaliableKnights.Enqueue(knight);
    }

    public void EnqueueChampion(GameObject champion)
    {
        _avaliableChampions.Enqueue(champion);
    }

    public GameObject GetSoldier()
    {
        if(_avaliableSoldiers.Count > 0)
        {
            return _avaliableSoldiers.Dequeue();
        }
        else
        {
            return Instantiate(_soldierPrefab);
        }
    }

    public GameObject GetKnight()
    {
        if (_avaliableKnights.Count > 0)
        {
            return _avaliableKnights.Dequeue();
        }
        else
        {
            return Instantiate(_knightPrefab);
        }
    }

    public GameObject GetChampion()
    {
        if (_avaliableChampions.Count > 0)
        {
            return _avaliableChampions.Dequeue();
        }
        else
        {
            return Instantiate(_championPrefab);
        }
    }
}
