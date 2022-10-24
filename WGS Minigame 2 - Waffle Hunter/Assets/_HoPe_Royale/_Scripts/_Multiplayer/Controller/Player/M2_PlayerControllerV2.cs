using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class M2_PlayerControllerV2 : MonoBehaviour
{
    protected static M2_PlayerControllerV2 s_Instance;
    public static M2_PlayerControllerV2 instance { get { return s_Instance; } }

    public CharacterController controller;
    public Animator anim;
    [Range(0, 10)] public float playerSpeed = 2.0f;

    [Header("Mobile Input")]
    [SerializeField] private FixedJoystick Joystick;

    Vector3 playerVelocity;
    float gravity = -9.8f;

    public bool canMove;

    PhotonView pv;

    private void Awake() => s_Instance = this;

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        Joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        // Joystick.gameObject.SetActive(true);
        // if (CheckPlatform.isMobile || CheckPlatform.isIos || CheckPlatform.isAndroid)
        // {
        //     
        // }
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (controller.isGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0;
            }

            playerVelocity.y += gravity * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            // if (CheckPlatform.isWeb || CheckPlatform.isWindowsUnity || CheckPlatform.isWindows)
            // {
            //     PlayerControllerMove();
            // }
            //
            JoystickMove();
        }
    }

    //! for WebGL and Desktop
    public void PlayerControllerMove()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1f);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            anim.SetBool("isRunning", true);
           
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    //! for mobile platform
    public void JoystickMove()
    {
        float _horizontal = Joystick.Horizontal;
        float _vertical = Joystick.Vertical;

        if (canMove)
        {
            Vector3 move = new Vector3(_horizontal, 0, _vertical);
            move = Vector3.ClampMagnitude(move, 1f);
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
                anim.SetBool("isRunning", true);
                if (!M2_PlayerAudioManager.instance.audioSource.isPlaying)
                {
                    M2_PlayerAudioManager.instance.PlayFootstepAudio();
                }
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
    }
}
