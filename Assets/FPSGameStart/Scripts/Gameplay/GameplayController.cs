using UnityEngine;
using System.Collections;
using System;

public class GameplayController : MonoBehaviour
{
    [SerializeField] private Transform playerSpawPositionOptions;
    [SerializeField] private GameObject playerPrefabs;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        GameObject playerClone = Instantiate(playerPrefabs);
        playerClone.transform.SetParent(this.transform);
        Transform randomSpawnPosition = GetRandomSpawnPoint();

        playerClone.transform.position = randomSpawnPosition.position;
    }

    private Transform GetRandomSpawnPoint()
    {
        Transform[] transformChilds = playerSpawPositionOptions.GetComponentsInChildren<Transform>();
        return transformChilds[UnityEngine.Random.Range(0, transformChilds.Length)];
    }
}
