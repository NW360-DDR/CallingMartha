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
    public GameObject titleScreenStuff;
    public GameObject creditScreenStuff;

    public AudioManager AudioManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Update()
    {
        if (prologueStarted)
        {
            text.rectTransform.Translate(Time.deltaTime * (text.rectTransform.position.y >= 0 ? 0 : scrollSpeed) * Vector2.up);
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
        AudioManager.PlayPlayButtonSound();
        prologueStarted = true;
        skip.SetActive(true);
        titleScreenStuff.SetActive(false);
    }

    public void CreditsClicked()
    {
        AudioManager.PlayOtherButtonSound();
        Debug.Log("Credits clicked!");
        titleScreenStuff.SetActive(false);
        creditScreenStuff.SetActive(true);
    }

    public void CloseCreditsClicked()
    {
        AudioManager.PlayOtherButtonSound();
        Debug.Log("Credits closed!");
        titleScreenStuff.SetActive(true);
        creditScreenStuff.SetActive(false);
    }

    public void MainMenuClicked()
    {
        AudioManager.PlayOtherButtonSound();
        SceneManager.LoadScene("Totally Legit Main Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Clicked!");
        Application.Quit();
    }
}