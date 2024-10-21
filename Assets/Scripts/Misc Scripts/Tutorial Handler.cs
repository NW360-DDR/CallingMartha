using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
	bool gamePaused = false;

    private void Update()
    {
        if (gamePaused && Input.GetKeyDown(KeyCode.E))
        {
			UncallScreen();
        }
    }
    public void CallScreen(string tag)
	{
		Debug.Log(tag);

		Time.timeScale = 0;
		// Show screen info here
		// When input happens, 

	}

	public void UncallScreen()
	{
		Time.timeScale = 1;
		// Show screen info here
		// When input happens, 

	}
}
