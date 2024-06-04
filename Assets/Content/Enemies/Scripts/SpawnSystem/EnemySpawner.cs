using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>();
    [SerializeField] private SpawnList _spawnList;
    [SerializeField] private bool _beginSpawningAtStart;


    //[SerializeField] private int _batchSize;
    [SerializeField] private float _initalSpawnInterval;

    [Header("Progression")]
    [SerializeField] private float _unlockTier2Time;
    [SerializeField] private float _unlockTier3Time;

    [Range(0f, 1f)]
    [SerializeField] private float _spawnIntervalPercentDecrease;
    [Range(0f, 1f)]
    [SerializeField] private float _spawnDifficultyPercentIncrease; // Every spawn, lower the difficulty multiplier to this much percent of its self (0-1)


    private float _spawnDifficultyMultiplier; // Lowers the random number generated for choosing enemy tier to increase chances for Higher Tier enemy
    private float _spawnInterval;

    private float _lastSpawn;
    private float _spawnTimer = 0;

    private bool _doSpawning = false;

    private bool _canSpawnTier2 = false;
    private bool _canSpawnTier3 = false;



    private void Start()
    {
        if (_beginSpawningAtStart)
        {
            _doSpawning = true;
        }
        _spawnInterval = _initalSpawnInterval;
        // _spawnDifficultyMultiplier = 1;

    }

    public void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_doSpawning)
        {


            if (_spawnTimer - _lastSpawn >= _spawnInterval)
            {
                SpawnEnemy();
                _lastSpawn = _spawnTimer;


                _spawnInterval -= _spawnInterval * _spawnIntervalPercentDecrease;
            }

            if (!_canSpawnTier2)
            {
                _canSpawnTier2 = Time.time > _unlockTier2Time;
            }

            if (!_canSpawnTier3)
            {
                _canSpawnTier3 = Time.time > _unlockTier3Time;
            }
        }
    }

    private void SpawnEnemy()
    {

        //Debug.Log("Difficulty Multiplier: " + _spawnDifficultyMultiplier);

        float typeRandom = Random.Range(0, _spawnList.TypeMaxRange);
        float tierRandom = Random.Range(0, _spawnList.TierMaxRange); //* _spawnDifficultyMultiplier

        EnemyType type;
        EnemyTier tier;

        if (typeRandom <= _spawnList.LongRangeChance)
        {
            type = EnemyType.LongRange;
        }
        else if (typeRandom <= _spawnList.MediumRangeChance)
        {
            type = EnemyType.MediumRange;
        }
        else
        {
            type = EnemyType.CloseRange;
        }

        if (_canSpawnTier3 && tierRandom <= _spawnList.Tier3Chance)
        {
            tier = EnemyTier.Tier3;
        }
        else if (_canSpawnTier2 && tierRandom <= _spawnList.Tier2Chance)
        {
            tier = EnemyTier.Tier2;
        }
        else
        {
            tier = EnemyTier.Tier1;
        }

        GameObject enemyToSpawn = _spawnList.GetEnemy(type, tier);
        EnemyController enemyController = enemyToSpawn.GetComponent<EnemyController>();

        Vector3 spawnOffset = new Vector3(transform.position.x + Random.Range(-5, 5), transform.position.y, transform.position.z + Random.Range(-5, 5));

        GameObject newEnemy = Instantiate(enemyToSpawn, this.transform.position, this.transform.rotation);

        //enemyController.EnableNavmeshAgency(this.transform);


        newEnemy.GetComponent<NavMeshAgent>().Warp(spawnOffset);
        //Debug.Log(batch[i].name);


        /* if (_canSpawnTier3)
        {
            _spawnDifficultyMultiplier -= _spawnDifficultyMultiplier * _spawnDifficultyPercentIncrease;
        } */

    }


    public void StopSpawning()
    {
        _doSpawning = false;
    }
}

public class SpawnCycle
{
    private float _lastSpawn;

    public bool Spawn()
    {

        if (Time.deltaTime - _lastSpawn >= 5f)
        {
            // Update the last spawn time
            _lastSpawn = Time.deltaTime;
            return true;
        }

        return false;
    }
}
