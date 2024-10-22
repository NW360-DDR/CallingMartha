using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
	bool gamePaused = false;
	GameObject currentTutorial;

    private void Update()
    {
        if (gamePaused && Input.GetKeyDown(KeyCode.E))
        {
			UncallScreen();
        }
    }
    public void CallScreen(GameObject screen)
	{
		Debug.Log(tag);

		Time.timeScale = 0;
		// Show screen info here
		gamePaused = true;
		currentTutorial = screen;

	}

	public void UncallScreen()
	{
		Time.timeScale = 1;
		// Make screen info... un-show up.
		gamePaused = false;
		currentTutorial.SetActive(false);
	}
}
