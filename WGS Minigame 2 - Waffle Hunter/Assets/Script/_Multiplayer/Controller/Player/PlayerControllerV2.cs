using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControllerV2 : MonoBehaviour
{
    public CharacterController controller;
    [SerializeField][Range(0, 10)] float playerSpeed = 2.0f;

    [Header("Mobile Input")]
    [SerializeField] private FixedJoystick Joystick;

    PhotonView pv;

    private void Start()
    {
        pv = GetComponent<PhotonView>();

        if (CheckPlatform.isMobile || CheckPlatform.isIos || CheckPlatform.isAndroid)
        {
            Joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
        }
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (CheckPlatform.isWeb || CheckPlatform.isWindowsUnity || CheckPlatform.isWindows)
            {
                PlayerControllerMove();
            }

            if (CheckPlatform.isMobile || CheckPlatform.isIos || CheckPlatform.isAndroid)
            {
                JoystickMove();
            }
        }
    }

    public void PlayerControllerMove()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
    }

    public void JoystickMove()
    {
        Vector2 move = Joystick.Direction * playerSpeed * Time.deltaTime;
        controller.Move(transform.right * move.x + transform.forward * move.y);
    }
}
