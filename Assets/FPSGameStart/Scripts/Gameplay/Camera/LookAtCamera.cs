using UnityEngine;
using Gameplay.Unit;

public class LookAtCamera : MonoBehaviour {

	private Transform target;

    //private GameplayController player;

    private PlayerUnit player;
	
    private void Awake()
    {
        player = FindObjectOfType(typeof(PlayerUnit)) as PlayerUnit;
        target = player.transform;
    }

    void OnEnable()
    {
        //player.OnPlayerSpawnedEvent += Player_OnPlayerSpawnedEvent;
    }

    void OnDisable()
    {
        //player.OnPlayerSpawnedEvent -= Player_OnPlayerSpawnedEvent;
    }

    private void Player_OnPlayerSpawnedEvent(PlayerUnit player)
    {
        target = player.transform;
    }

    void LateUpdate() {

        if (target == null)
            return;

		transform.LookAt(target);
	}
}
