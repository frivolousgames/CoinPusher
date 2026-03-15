using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    string[] buttonNames;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] clips;
    AudioClip selectedClip;
    bool clicked;

    [SerializeField]
    GameObject confirmNewGamePanel;

    [SerializeField]
    GameObject loadScreen;

    private void Awake()
    {
        buttonNames = new string[]
        {
            "Play", "New Game", "Quit"
        };
        clicked = false;
    }

    public void Play()
    {
        audioSource.clip = clips[0];
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        var asyncload = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
        asyncload.allowSceneActivation = false;
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(.4f);
        PlayerPrefs.SetInt("NewGame", 1);
        loadScreen.SetActive(true);
        asyncload.allowSceneActivation = true;
    }

    public void NewGame()
    {
        if(PlayerPrefs.GetInt("NewGame", 0)  == 0)
        {
            audioSource.clip = clips[1];
            StartCoroutine(PlayRoutine());
        }
        else
        {
            confirmNewGamePanel.SetActive(true);
        }
    }

    public void NewGameYes()
    {
        audioSource.clip = clips[1];
        PlayerPrefs.DeleteAll();
        StartCoroutine(PlayRoutine());
    }

    public void NewGameNo()
    {
        TitleButton.isClicked = false;
        clicked = false;
        confirmNewGamePanel.SetActive(false);
    }

    public void Quit()
    {
        audioSource.clip = clips[2];
        StartCoroutine(QuitRoutine());
    }

    IEnumerator QuitRoutine()
    {
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(.4f);
        Application.Quit();
    }
}
