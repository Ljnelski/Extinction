using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    [SerializeField] private SpawnList _spawnList;
    [SerializeField] private bool _beginSpawningAtStart;


    [SerializeField] private int _batchSize;
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
        _spawnDifficultyMultiplier = 1;
    }

    public void Update()
    {
        if (_doSpawning)
        {
            if (Time.time - _lastSpawn >= _spawnInterval)
            {
                Debug.LogWarning("SPAWNING!");
                CreateEnemyBatch();
                _lastSpawn = Time.time;

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

    private GameObject[] CreateEnemyBatch()
    {
        GameObject[] batch = new GameObject[_batchSize];

        //Debug.Log("Difficulty Multiplier: " + _spawnDifficultyMultiplier);

        for (int i = 0; i < batch.Length; i++)
        {

            float typeRandom = Random.Range(0, _spawnList.TypeMaxRange);
            float tierRandom = Random.Range(0, _spawnList.TierMaxRange) * _spawnDifficultyMultiplier;

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

            batch[i] = _spawnList.GetEnemy(type, tier);

            if (batch[i] == null)
            {
                i--;
            }
        }

        for (int i = 0; i < batch.Length; i++)
        {
            Debug.Log(batch[i].name);
        }

        if (_canSpawnTier3)
        {
            _spawnDifficultyMultiplier -= _spawnDifficultyMultiplier * _spawnDifficultyPercentIncrease;
        }

        return batch;
    }


    public void StopSpawning()
    {
        _doSpawning = false;
    }
}

public class SpawnCycle
{
    private float _lastSpawn;
    private int _batchSize;

    public SpawnCycle()
    {
        _batchSize = 10;
    }

    public bool Spawn()
    {
        if (Time.time - _lastSpawn >= 5f)
        {
            // Update the last spawn time
            _lastSpawn = Time.time;
            return true;
        }

        return false;
    }
}
