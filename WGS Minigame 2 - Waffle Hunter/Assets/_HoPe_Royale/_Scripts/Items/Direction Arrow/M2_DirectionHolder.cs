using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class M2_DirectionHolder : MonoBehaviour
{
    public GameObject Direction;
    public float itemTime;
    public bool isShowing;

    PhotonView view;

    private void Start() => view = GetComponent<PhotonView>();

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Direction")
        {

            ShowDirection(col);
            Destroy(col.gameObject, itemTime);
        }
    }

    public void ShowDirection(Collider col)
    {
        if (view.IsMine)
        {
            M2_ObjectAudioManager.instance.PlayDirectionAudio();
            isShowing = true;
            Direction.SetActive(true);
        }
        Invoke("hidDirection", itemTime);
        col.GetComponentInChildren<MeshRenderer>().enabled = false;
        col.GetComponent<BoxCollider>().enabled = false;
    }

    public void hidDirection()
    {
        Direction.SetActive(false);
        isShowing = false;
    }
}
