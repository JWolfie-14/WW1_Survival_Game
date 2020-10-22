using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStatusManager : MonoBehaviour
{
    public CursorManager cursorManager;
    public TabPanelManager tabPanelManager;
    public bool hasWave1Played = false;
    public bool isPaused = false;
    public GameObject mainMenuPanel, pauseMenuPanel, gameUIPanel, quitConfirmationPanel;


    void Update() {
        {
            if (Input.GetButtonDown("Cancel") && !tabPanelManager.isTabMenuActive)
            {
                PauseLoop();
            }
        }
    }
    public void PauseLoop() 
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            cursorManager.ActivateCursor();
        }
        else{
            cursorManager.DeactivateCursor();
        }
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        gameUIPanel.SetActive(!gameUIPanel.activeSelf);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitConfirmation()
    {
        isPaused = false;
        gameUIPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
        quitConfirmationPanel.SetActive(true);
    }
    
    public void Quit() 
    {
        Debug.Log("Game Quited");
        Application.Quit();
    }
}
