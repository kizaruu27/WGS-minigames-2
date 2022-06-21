
using UnityEngine;
using Photon.Pun;

public class GameFlowManager : MonoBehaviour
{

    public static GameFlowManager instance;

    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject[] disableOnFinish;


    [SerializeField] InGameTimer timer;
    WaffleHandler waffleHandler;

    public bool isDone;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        waffleHandler = FindObjectOfType<WaffleHandler>();
    }


    void Update()
    {
        // Pause();

        pv.RPC(nameof(RPC_GameIsDone), RpcTarget.AllBuffered, waffleHandler.isWin || timer.duration == 0);
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
        this.isDone = isDone;

        if (isDone)
        {
            DisableGO();
            WinUI.SetActive(true);
        }
    }
}
