using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float movementSpeed = 10f;
    public float runningSpeed = 15f;
    public bool enableJump = true;
    public float jumpHeight = 3f;
    public float gravity = 9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    // public float rollSpeed = 100f;

    Vector3 velocity;
    bool isGrounded;
    bool isRunning;

    void Start()
    {
        isGrounded = false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            controller.Move(move * runningSpeed * Time.deltaTime);
        }
        else
        {
            isRunning = false;
            controller.Move(move * movementSpeed * Time.deltaTime);
        }
        

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("JUMP");
            float y = Mathf.Sqrt(jumpHeight * 2 * gravity);
            Debug.Log(y);
            velocity.y = y;
        }

        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     StartCoroutine(Roll(1));
        // }

        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     StartCoroutine(Roll(-1));
        // }

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // IEnumerator Roll(int direction)
    // {
    //     float rollAmount = 90f * direction;
    //     float rollTime = 0.5f;
    //     float elapsedTime = 0f;
    //     Quaternion startRotation = transform.rotation;
    //     Quaternion endRotation = transform.rotation * Quaternion.Euler(0f, rollAmount, 0f);

    //     while (elapsedTime < rollTime)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         float t = elapsedTime / rollTime;
    //         transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
    //         yield return null;
    //     }
    // }

    public bool GetisGrounded()
    {
        if (isGrounded)
        {
            return true;
        }
        return false;
    }
    public bool GetisRunning()
    {
        if (isRunning)
        {
            return true;
        }
        return false;
    }
}