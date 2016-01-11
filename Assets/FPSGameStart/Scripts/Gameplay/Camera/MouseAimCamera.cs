using UnityEngine;
using Gameplay.Unit;

public class MouseAimCamera : MonoBehaviour {

	public float rotateSpeed = 5;
	Vector3 offset;


    public Transform target;
    //private GameplayController player;

    private PlayerUnit player;


    private void Awake()
    {
        player = FindObjectOfType(typeof(PlayerUnit)) as PlayerUnit;
    }

    void Start() {
        target = player.transform;
        offset = target.position - transform.position;
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
		offset = target.position - transform.position;
    }

    void LateUpdate() {

        if (target == null)
            return;

		float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
		target.Rotate(0, horizontal, 0);

		float desiredAngle = target.eulerAngles.y;
		Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		transform.position = target.position - (rotation * offset);
		
		transform.LookAt(target.transform);
	}
}