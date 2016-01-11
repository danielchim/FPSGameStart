using UnityEngine;
using System.Collections;

public class CameraTeste : MonoBehaviour {
    //This camera
    public Camera mainCamera;
    //Our character
    public Transform player;
    //distance between character and camera
    public float distance = 5.0f;
    //x and y position of camera
    float x = 0.0f;
    float y = 0.0f;
    //x and y side speed, how fast your camera moves in x way and in y way
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    //Minium and maximum distance between player and camera
    public float distanceMin = 0.5f;
    public float distanceMax = 15f;
    //checks if first person mode is on
    private bool click = false;
    //stores cameras distance from player
    private float curDist = 0;

    private void Start()
    {
        //make variable from our euler angles
        Vector3 angles = transform.eulerAngles;
        //and store y and x angles to different values
        x = angles.y;
        y = angles.x;
        //sets this camera to main camera
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        //gets mouse movement x and y and multiplies them with speeds and moves camera with them
        x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        //set rotation
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        //changes distance between max and min distancy by mouse scroll
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
        //negative distance of camera
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        //cameras postion
        Vector3 position = rotation * negDistance + player.position;
        //rotation and position of our camera to different variables
        transform.rotation = rotation;
        transform.position = position;
        //cameras x rotation
        float cameraX = transform.rotation.x;
        //checks if right mouse button is pushed
        if (Input.GetMouseButton(1))
        {
            //sets CHARACTERS x rotation to match cameras x rotation
            player.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        //checks if middle mouse button is pushed down
        if (Input.GetMouseButtonDown(2))
        {
            //if middle mouse button is pressed 1st time set click to true and camera in front of player and save cameras position before mmb.
            //if mmb is pressed again set camera back to it's position before we clicked mmb 1st time and set click to false
            if (click == false)
            {
                click = true;
                curDist = distance;
                distance = distance - distance - 1;
            }
            else
            {
                distance = curDist;
                click = false;
            }
        }
        //store raycast hit
        RaycastHit hit;
        //if camera detects something behind or under it move camera to hitpoint so it doesn't go throught wall/floor
        if (Physics.Raycast(player.position, (transform.position - player.position).normalized, out hit, (distance <= 0 ? -distance : distance)))
        {
            transform.position = hit.point;
        }
    }
}