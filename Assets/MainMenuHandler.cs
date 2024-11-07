using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public string MainGame;
    bool prologueStarted;
    public float scrollSpeed = 0.0f;
    public TextMeshProUGUI text;
    public GameObject skip;

     void Update()
    {
        if (prologueStarted)
        {
            text.rectTransform.Translate(Time.deltaTime * scrollSpeed * Vector2.up);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(StartGame());
            }
        }
    }

  

    IEnumerator StartGame()
    {
        prologueStarted = true;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(MainGame);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void Prologue()
    {
        prologueStarted = true;
        skip.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

