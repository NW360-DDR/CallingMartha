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
    public void CallScreen()
	{
		Debug.Log(tag);

		Time.timeScale = 0;
		// Show screen info here
		gamePaused = true;

	}

	public void UncallScreen()
	{
		Time.timeScale = 1;
		// Make screen info... un-show up.
		gamePaused = false;
	}
}
