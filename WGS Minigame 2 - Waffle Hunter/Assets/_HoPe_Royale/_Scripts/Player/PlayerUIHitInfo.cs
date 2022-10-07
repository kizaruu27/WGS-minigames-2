using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerUIHitInfo : MonoBehaviour
{
    public Text textPrefab;
    Text UIUse;
    public float yPos;
    public bool isActive;
    PhotonView view;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            if (isActive)
            {
                UIUse.transform.position = Camera.main.WorldToScreenPoint(new Vector3 (transform.position.x, transform.position.y + yPos, transform.position.z));
            }
        }
    }

    public void CallUINotif(string notif)
    {
        if (view.IsMine)
        {
            isActive = true;
            textPrefab.text = notif;
            UIUse = Instantiate(textPrefab, FindObjectOfType<Canvas>().transform).GetComponent<Text>();

            Destroy(textPrefab, 0.5f);
        }
    }
}
