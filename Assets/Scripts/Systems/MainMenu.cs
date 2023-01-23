using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayLoop(SoundManager.Sounds.MainMenuBGM);
    }

    public void QuitGame()
    {
        SoundManager.Instance.StopPlayingBGM(SoundManager.Sounds.MainMenuBGM);
        Application.Quit();
    }

    public void PlayGame()
    {
        SoundManager.Instance.StopPlayingBGM(SoundManager.Sounds.MainMenuBGM);
        SceneManager.LoadScene("MainGame");
    }
}
