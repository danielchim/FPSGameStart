using UnityEngine;
using Gameplay.Unit;

public class DungeonCamera : MonoBehaviour {
	public Transform target;
	public float damping = 1;
	Vector3 offset;

    //private GameplayController player;

    private PlayerUnit player;

    private void Awake()
    {
        player = FindObjectOfType(typeof(PlayerUnit)) as PlayerUnit;
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
    }


    void Start() {
        target = player.transform;
		offset = transform.position - target.position;
	}
	
	void LateUpdate() {

        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
		Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
		transform.position = position;

		transform.LookAt(target.position);
	}
}
