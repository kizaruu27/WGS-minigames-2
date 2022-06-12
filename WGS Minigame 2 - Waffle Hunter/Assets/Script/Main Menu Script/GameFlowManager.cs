using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject WinUI;

    [SerializeField] InGameTimer timer;
    WaffleHandler waffleHandler;

    private void Start()
    {
        Time.timeScale = 1;
        waffleHandler = FindObjectOfType<WaffleHandler>();
    }
    

    void Update()
    {
        Pause();

        if (waffleHandler.isWin || timer.timer == 0)
        {
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




}
