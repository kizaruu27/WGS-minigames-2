using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class M2_LeaveRoom : MonoBehaviour
{
    public string targetScene;
    
    public void LeaveRoom()
    {
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(targetScene);
    }
}
