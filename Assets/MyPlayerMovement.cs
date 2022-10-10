using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyPlayerMovement : MonoBehaviour
{
    public int Health = 100;


    public CharacterController myMovement;
    public Camera myCamera;
    public float movementSpeed = 10;
    bool isMoving = false;

    public float jumpHeight = 100;
    bool isJumping = false;

    public float turnTime = 0.1f;
    public float turnVelocity = 0.1f;

    public float gravity = -20f;
    Vector3 velocity;

    bool boolIsGrounded = false;

    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

        myMovement = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            isMoving = true;
        }

        if (Input.GetButton("Jump"))
        {
            isJumping = true;
        }

        boolIsGrounded = myMovement.isGrounded;


        if (boolIsGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (isJumping && boolIsGrounded)
        {
            velocity.y += jumpHeight * Time.deltaTime;
        }

        velocity.y += gravity*Time.deltaTime;

        myMovement.Move(velocity*Time.deltaTime);
        if (isMoving)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            if (move.magnitude >= 0.5)
            {
                float targetFace = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + myCamera.transform.eulerAngles.y;
                float smoothTargetFace = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetFace, ref turnVelocity, turnTime);
                transform.rotation = Quaternion.Euler(0f, smoothTargetFace, 0);

                Vector3 direction = Quaternion.Euler(0f, targetFace, 0f) * Vector3.forward;
                direction = direction.normalized;
                myMovement.Move(direction * movementSpeed * Time.deltaTime);
            }
        }

        isMoving = false;
        isJumping = false;

    }


}
