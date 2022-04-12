using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Variable")]
    CharacterController _controller;
    [SerializeField] public float playerSpeed = 5f;
    [SerializeField] float rotationSpeed = 10f;

    [Header("Camera Variable")]
    Camera followCamera;

    [Header("Jump Variable")]
    Vector3 playerVelocity;
    bool _isGrounded;
    [SerializeField][Range(0, 1)] float jumpHeight = 1.0f;
    [SerializeField] float gravity = -9.8f;

    //Animation variable
    Animator anim;
    bool touchGround;
    public SpeedUp _speedUp;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        _speedUp = GetComponent<SpeedUp>();
        followCamera = Camera.main;
    }

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        //Player Gravity
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        //Player Move
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        Vector3 movementDirection = movementInput.normalized;

        _controller.Move(movementDirection * playerSpeed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        //Jump();

        playerVelocity.y += gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    void Jump()
    {
        //Player Jump
        if (Input.GetButton("Jump") && _isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            //anim.SetBool("isJumping", true);
        }
        else
        {
            //anim.SetBool("isJumping", false);
        }
    }

}
