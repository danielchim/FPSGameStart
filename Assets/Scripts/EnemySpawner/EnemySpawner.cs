using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnEnemyInterval = 5.0f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private BaseEnemy baseEnemyPrefabs;

    private List<BaseEnemy> aliveEnemies = new List<BaseEnemy>();
    private bool alive;

    private void Start()
    {
        StartCoroutine(SpawnEnemyCoroutine());
    }

    private IEnumerator SpawnEnemyCoroutine()
    {
        alive = true;
        while (alive)
        {
            if (CanSpawnMoreEnemies())
            {
                SpawnNewEnemy();
            }

            yield return new WaitForSeconds(spawnEnemyInterval);
              
        }
    }

    private void SpawnNewEnemy()
    {
        BaseEnemy baseEnemyClone = Instantiate(baseEnemyPrefabs);
        baseEnemyClone.transform.SetParent(this.transform);
        baseEnemyClone.transform.localPosition = Vector3.zero;

        aliveEnemies.Add(baseEnemyClone);
    }

    private bool CanSpawnMoreEnemies()
    {
        if (aliveEnemies.Count < maxEnemies)
            return true;
        return false;
    }
}
