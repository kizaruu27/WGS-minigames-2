
using UnityEngine;
using Photon.Pun;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject[] disableOnFinish;


    [SerializeField] InGameTimer timer;
    WaffleHandler waffleHandler;

    bool _isDone;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        Time.timeScale = 1;
        waffleHandler = FindObjectOfType<WaffleHandler>();
    }


    void Update()
    {
        // Pause();

        pv.RPC("RPC_GameIsDone", RpcTarget.AllBuffered, waffleHandler.isWin || timer.timer == 0);

        if (_isDone)
        {
            DisableGO();
            WinUI.SetActive(true);
        }
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI.SetActive(true);
        }
    }

    public void ResumeGame(GameObject UI)
    {
        Time.timeScale = 1;
        UI.SetActive(false);
    }


    public void DisableGO()
    {
        foreach (var go in disableOnFinish) go.SetActive(false);
    }


    [PunRPC]
    public void RPC_GameIsDone(bool isDone)
    {
        _isDone = isDone;
    }
}
