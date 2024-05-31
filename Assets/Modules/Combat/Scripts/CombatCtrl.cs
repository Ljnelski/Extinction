using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCtrl : MonoBehaviour
{
    static CombatCtrl _instance;
    public static CombatCtrl Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<CombatCtrl>();
            }

            return _instance;
        }
    }

    public Transform player;

    [SerializeField] List<SpawnData> spawns;

    List<SpawnInfo> spawnInfo;

    private void Start()
    {
        spawnInfo = new List<SpawnInfo>();
        foreach (var spawnData in spawns)
        {
            var info = new SpawnInfo(spawnData);
            spawnInfo.Add(info);
        }
    }

    private void Update()
    {
        foreach (var info in spawnInfo)
        {
            info.Update();
        }
    }

    [System.Serializable]
    public class SpawnData
    {
        public List<Transform> spawnPoints;
        public GameObject prefab;
        public float rate;
    }

    public class SpawnInfo
    {
        SpawnData data;
        public float lastSpawn;

        public SpawnInfo(SpawnData spawnData)
        {
            data = spawnData;
            lastSpawn = Time.time;
        }

        public void Update()
        {
            if (Time.time - lastSpawn >= data.rate)
            {
                int spawnIndex = Random.Range(0, data.spawnPoints.Count);
                Transform spawnPoint = data.spawnPoints[spawnIndex];

                // Instantiate the prefab at the chosen spawn point
                Instantiate(data.prefab, spawnPoint.position, spawnPoint.rotation);

                // Update the last spawn time
                lastSpawn = Time.time;
            }
        }
    }
}