using UnityEngine;
using System.Collections;
using System;
using Gameplay.Unit;

public class GameplayController : MonoSingleton<GameplayController>
{
    public delegate void OnPlayerSpawnedDelegate(PlayerUnit player);
    public event OnPlayerSpawnedDelegate OnPlayerSpawnedEvent;

    [SerializeField]
    private Transform playerSpawPositionOptions;
    [SerializeField]
    private GameObject playerPrefabs;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        PlayerUnit playerClone = Instantiate(playerPrefabs).GetComponent<PlayerUnit>();
        playerClone.transform.SetParent(this.transform);
        Transform randomSpawnPosition = GetRandomSpawnPoint();

        playerClone.transform.position = randomSpawnPosition.position;

        DispatchOnPlayerSpawnEvent(playerClone);
    }

    private void DispatchOnPlayerSpawnEvent(PlayerUnit _playerClone)
    {
        if (OnPlayerSpawnedEvent != null)
            OnPlayerSpawnedEvent(_playerClone);
    }

    private Transform GetRandomSpawnPoint()
    {
        Transform[] transformChilds = playerSpawPositionOptions.GetComponentsInChildren<Transform>();
        return transformChilds[UnityEngine.Random.Range(0, transformChilds.Length)];
    }
}
