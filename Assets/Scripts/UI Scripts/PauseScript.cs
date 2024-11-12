using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private EclipseTimer eclipseScript;
    public GameObject pauseMenu;
    private void Start()
    {
        eclipseScript = GameObject.Find("EclipseTimer").GetComponent<EclipseTimer>();
    }
    public void ContinueClick()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        eclipseScript.gameTimerActive = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Debug.Log("Clicked Continue!");
    }

    public void QuitClick()
    {
        Debug.Log("Clicked Quit!");
        Application.Quit();
    }
}
