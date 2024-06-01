using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnList", menuName = "Extinction/Spawning/SpawnList")]

public class SpawnList : ScriptableObject
{
    [SerializeField] private GameObject _closeRangeTier1;
    [SerializeField] private GameObject _closeRangeTier2;
    [SerializeField] private GameObject _closeRangeTier3;

    [SerializeField] private GameObject _mediumRangeTier1;
    [SerializeField] private GameObject _mediumRangeTier2;
    [SerializeField] private GameObject _mediumRangeTier3;

    [SerializeField] private GameObject _longRangeTier1;
    [SerializeField] private GameObject _longRangeTier2;
    [SerializeField] private GameObject _longRangeTier3;

    [Range(0f, 100f)][SerializeField] private float _closeRangeChance = 50f;
    [Range(0f, 100f)][SerializeField] private float _mediumRangeChance = 25f;
    [Range(0f, 100f)][SerializeField] private float _longRangeChance = 15f;

    [Range(0f, 100f)][SerializeField] private float _tier1Chance = 75f;
    [Range(0f, 100f)][SerializeField] private float _tier2Chance = 20f;
    [Range(0f, 100f)][SerializeField] private float _tier3Chance = 5f;

    // [EnemyType, EnemyTier]
    private GameObject[,] Enemies
    {
        get
        {
            if (_enemies == null)
            {
                _enemies = new GameObject[3, 3];

                _enemies[(int)EnemyType.CloseRange, (int)EnemyTier.Tier1] = _closeRangeTier1;
                _enemies[(int)EnemyType.CloseRange, (int)EnemyTier.Tier2] = _closeRangeTier2;
                _enemies[(int)EnemyType.CloseRange, (int)EnemyTier.Tier3] = _closeRangeTier3;

                _enemies[(int)EnemyType.MediumRange, (int)EnemyTier.Tier1] = _mediumRangeTier1;
                _enemies[(int)EnemyType.MediumRange, (int)EnemyTier.Tier2] = _mediumRangeTier2;
                _enemies[(int)EnemyType.MediumRange, (int)EnemyTier.Tier3] = _mediumRangeTier3;

                _enemies[(int)EnemyType.LongRange, (int)EnemyTier.Tier1] = _longRangeTier1;
                _enemies[(int)EnemyType.LongRange, (int)EnemyTier.Tier2] = _longRangeTier2;
                _enemies[(int)EnemyType.LongRange, (int)EnemyTier.Tier3] = _longRangeTier3;
            }

            return _enemies;
        }
    }

    public float CloseRangeChance => _closeRangeChance + _mediumRangeChance + _longRangeChance;
    public float MediumRangeChance => _mediumRangeChance + _longRangeChance;
    public float LongRangeChance => _longRangeChance;
    public float Tier1Chance => _tier1Chance + _tier2Chance + Tier3Chance;
    public float Tier2Chance => _tier2Chance + Tier3Chance;
    public float Tier3Chance => _tier3Chance;

    public float TierMaxRange => _tier1Chance + _tier2Chance + _tier3Chance;
    public float TypeMaxRange => _closeRangeChance + _mediumRangeChance + _longRangeChance;

    private GameObject[,] _enemies;

    public GameObject GetEnemy(EnemyType type, EnemyTier tier)
    {
        return Enemies[(int)type, (int)tier];
    }

}


