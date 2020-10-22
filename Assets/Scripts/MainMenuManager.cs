using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleMenu(GameObject nextPanel)
    {
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
        nextPanel.SetActive(!nextPanel.activeSelf);
    }

    public void Quit() 
    {
        Debug.Log("Game Quited");
        Application.Quit();
    }
}
