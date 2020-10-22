using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabPanelManager : MonoBehaviour
{
    public KeyCode tab;
    public GameStatusManager GameStatusManager;
    public GameObject tabPanel;
    public bool isTabMenuActive = false;
    public GameObject removeCursor;
    public GameObject player;
    public GameObject thirdPersonCam;
    
    void Update()
    {
        if (!GameStatusManager.isPaused)
        {
            if (Input.GetKeyDown(tab))
            {
                player.GetComponent<BulletFiringScript>().UIActive = true;
                isTabMenuActive = true;
                removeCursor.GetComponent<CursorManager>().ActivateCursor();
                tabPanel.SetActive(true);
                thirdPersonCam.SetActive(false);
            }
            if (Input.GetKeyUp(tab))
            {
                player.GetComponent<BulletFiringScript>().UIActive = false;
                isTabMenuActive = false;
                removeCursor.GetComponent<CursorManager>().DeactivateCursor();
                tabPanel.SetActive(false);
                thirdPersonCam.SetActive(true);
            }
        }

    }
}
