using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class CameraController : MonoBehaviour
{

    PhotonView view;

    private void Awake() => view = GetComponent<PhotonView>();
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            GameObject
                .FindGameObjectWithTag("CMvcam")
                .GetComponent<CinemachineVirtualCamera>().Follow = this.transform;
        }
    }
}
