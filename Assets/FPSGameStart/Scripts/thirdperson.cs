using UnityEngine;
using System.Collections;

public class thirdperson : MonoBehaviour
{

    //Walking speed
    public float speed = 6;
    //Running speed of our character
    public float runSpeed = 9;
    //Speed of how fast our character turns
    public float rotateSpeed = 90;
    // is character grounded
    public bool grounded;
    //is character jumping
    public bool isJumping;
    // which way our character is moving. Defaul value is zero
    public Vector3 moveDirection = Vector3.zero;

    private Rigidbody _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        //in start we set grounded and isjumping to false
        grounded = false;
        isJumping = false;
    }

    void Update()
    {
        //we set rigidbodys velocity to 0
        _rigidBody.velocity = Vector3.zero;
        //checks if there is anything under character if is set grounded to true if not set it to false
        if (Physics.Raycast(transform.GetChild(0).position, Vector3.down, transform.localScale.y / 2))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        //checks if we are grounded so we cant jump again in air and if we press jump button. else we are not jumping so isjumping = false
        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            //Starts jump ienumerator
            StartCoroutine("jump");
        }
else
{
            isJumping = false;
        }
        //If we press "d" we rotate our player as long as "d" is pressed down and if we press "a" rotate other way
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, Mathf.Clamp(180f * Time.deltaTime, 0f, 360f));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -Mathf.Clamp(180f * Time.deltaTime, 0f, 360f));
        }
        //set out movedirection equal to our horizontal and vertical axis and out transform direction to those axis.
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);

        // checks if we push left shift if yes it multiplies our movedirection speed with runspeed variable if not we go with normal speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= runSpeed;
        }
        else
        {
            moveDirection *= speed;
        }
        //Checks if our character is not jumping and its not grounded. then moves character down "gravity"
        if (isJumping == false && grounded == false)
{
            transform.Translate(Vector3.down * 3 * Time.deltaTime);
        }
        //we set rigidbodys velocity to our movedirection wich contains speed and rotation of our character
        _rigidBody.velocity = moveDirection;

    }

    IEnumerator jump()
    {
        //First we ste player to jumping so gravity dsnt affect it
        isJumping = true;
        //We make loop that moves our player up multiple times if its not in touch with anything in front of it
        for (int i = 0; i < 30; i++)
{
            if (Physics.Raycast(transform.position, Vector3.forward, transform.localScale.y / 2 + 0.5f))
            {
                break;
            }
            else
            {
                transform.Translate(Vector3.up * 15 * Time.deltaTime, Space.World);
            }
            yield return null;
        }
        //last we set jumping back to false
        isJumping = false;
    }
}