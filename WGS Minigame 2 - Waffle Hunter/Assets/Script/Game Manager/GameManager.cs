using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject previewCamera;
    PhotonView view;

    [SerializeField] GameObject Joystick;
    [SerializeField] GameObject AttackButton;

    private void Awake()
    {
        view = GetComponent<PhotonView>();

        Joystick.SetActive(view.IsMine && CheckPlatform.isAndroid || CheckPlatform.isIos || CheckPlatform.isMobile);
        AttackButton.SetActive(view.IsMine && CheckPlatform.isAndroid || CheckPlatform.isIos || CheckPlatform.isMobile);
    }

    // Start is called before the first frame update
    void Start()
    {
        previewCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
