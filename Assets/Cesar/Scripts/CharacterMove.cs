using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private Rigidbody playerRb;

    public float walkSpeed = 3;
    public float runSpeed = 5;
    public float crouchSpeed = 7;

    void Start()
    {
        
        playerRb = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        playerRb.velocity = new Vector3(transform.position.x * HorizontalInput(), 0, transform.position.z * VerticalInput()) * ActualSpeed();
    }

    private float ActualSpeed()
    {
        return IsRunning() ? runSpeed : IsCrouching() ? crouchSpeed : walkSpeed;
    }

    private float HorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    private float VerticalInput()
    {
        return Input.GetAxis("Vertical");
    }

    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private bool IsCrouching()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }

}
