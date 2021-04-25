using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public AudioSource buttonPressed;
    public GameObject pauseMenuUI;
    public GameObject backgroundMusic;
    private const float freezeTime = 0f;
    private const float normalTime = 1f;
    private const float pausedMusicVolume = 0.5f;
    private const float normalMusicVolume = 2f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        backgroundMusic.GetComponent<AudioSource>().volume *= normalMusicVolume;
        pauseMenuUI.SetActive(false);
        Time.timeScale = normalTime;
        isPaused = false;
    }

    void Pause()
    {
        backgroundMusic.GetComponent<AudioSource>().volume *= pausedMusicVolume;
        pauseMenuUI.SetActive(true);
        Time.timeScale = freezeTime;
        isPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = normalTime;
        isPaused = false; // without this, we will not play the looping player bullet sound effect until the player presses esc during the newly started game
        // For the sake of finishing the game, I will not fix this bug, however, this solution makes it so that a 1 second clip of the loop of player bullet sound effect will play
        // just before we go back to the main menu. It is not necessarily game breaking, but it is a minor flaw.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonPressed()
    {
        buttonPressed.Play();
    }
}
