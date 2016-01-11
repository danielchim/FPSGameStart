using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {

    private Rigidbody _rigidBody;
    public float speed = 5f;

    [SerializeField]
    private LayerMask groundLayer;

    private Quaternion mouseRotation = Quaternion.identity;

    Vector3 lookTarget;

    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        _rigidBody.velocity = new Vector3(h * speed * Time.deltaTime, 0, v * speed * Time.deltaTime);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, groundLayer.value))
        {
            lookTarget = hit.point;

            Vector3 diff = hit.point - transform.position;
            diff.y = 0;
            mouseRotation = Quaternion.LookRotation(diff);
            //transform.LookAt(lookTarget);


            Debug.DrawRay(transform.position, diff, Color.green);



        }

         //_rigidBody.MoveRotation(mouseRotation);
        //transform.InverseTransformDirection(_rigidBody.transform.forward);
    }
}


/*

 Vector3 mouse_pos = Input.mousePosition;
        Vector3 player_pos = Camera.main.WorldToScreenPoint(this.transform.position);

        mouse_pos.x = mouse_pos.x - player_pos.x;
        mouse_pos.y = mouse_pos.y - player_pos.y;

        float angle = Mathf.Atan2 (mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler (new Vector3(0, 0, angle));

*/
