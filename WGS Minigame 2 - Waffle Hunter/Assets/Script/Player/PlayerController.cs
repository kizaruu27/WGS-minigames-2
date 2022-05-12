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
    

    [Header("Joypad Variable")]
    //[SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _animator;

    //[SerializeField] private float _moveSpeed;

    //Animation variable
    Animator anim;
    bool touchGround;

    private void Awake()
    {
        _joystick.gameObject.SetActive(CheckPlatform.isAndroid || CheckPlatform.isIos);
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        followCamera = Camera.main;
    }

    private void Update()
    {
        Movement();
    }

    //Joy Pad Controller
    private void FixedUpdate()
    {
        //Player Gravity
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }

        //Player Move
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        Vector3 movementDirection = movementInput.normalized;

        _controller.Move(movementDirection * playerSpeed * Time.deltaTime);

        //_rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
            anim.SetBool("isWalking", true);
            
            /*
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            anim.SetBool("isRunning", true);
            */
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
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
