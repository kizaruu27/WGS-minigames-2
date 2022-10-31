
using UnityEngine;
using Photon.Pun;

public class M2_GameFlowManager : MonoBehaviour
{

    public static M2_GameFlowManager instance;

    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject WinUI;
    [SerializeField] GameObject[] disableOnFinish;

    [SerializeField] M2_InGameTimer timer;
    M2_WaffleHandler _m2WaffleHandler;

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
        _m2WaffleHandler = FindObjectOfType<M2_WaffleHandler>();
    }


    void Update()
    {
        // Pause();

        pv.RPC(nameof(RPC_GameIsDone), RpcTarget.AllBuffered, _m2WaffleHandler.isWin || timer.duration == 0);
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

            M2_PlayerControllerV2[] playercontroller = FindObjectsOfType<M2_PlayerControllerV2>();
            foreach (var controller in playercontroller)
            {
                controller.enabled = false;
            }

            M2_NpcController npcController = FindObjectOfType<M2_NpcController>();
            npcController.enabled = false;
        }
    }
}
