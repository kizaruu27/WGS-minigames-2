using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] GameObject PauseUI;

    private void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame(GameObject UI)
    {
        Time.timeScale = 1;
        UI.SetActive(false);
    }
}
