using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public GameObject previewCamera;
    public GameObject _canvas;
    PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        _canvas.SetActive(CheckPlatform.isMobile || CheckPlatform.isIos || CheckPlatform.isAndroid);
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
