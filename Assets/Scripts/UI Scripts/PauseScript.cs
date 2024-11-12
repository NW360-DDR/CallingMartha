using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private EclipseTimer eclipseScript;
    private GameObject pauseMenu;
    private void Start()
    {
        eclipseScript = GameObject.Find("EclipseTimer").GetComponent<EclipseTimer>();
        pauseMenu = GameObject.Find("Pause Menu");
    }
    public void ContinueClick()
    {
        pauseMenu.SetActive(false);
        eclipseScript.gameTimerActive = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Clicked Continue!");
    }

    public void QuitClick()
    {
        Debug.Log("Clicked Quit!");
    }
}
