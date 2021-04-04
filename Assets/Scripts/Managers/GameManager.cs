using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver;
    private bool isGamePaused;

    private UIManager uIManager;

    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            ResumeGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene(2); // Current Game Scene
        }
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        uIManager.UpdatePauseUI(isGamePaused);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        uIManager.UpdatePauseUI(isGamePaused);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(2); // Current Game Scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
