using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver;
    private bool isGamePaused;

    private UIManager uIManager;
    private static GameManager instance;
    public Vector2 lastCheckpoint;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused && !isGameOver)
        {
            ResumeGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused && !isGameOver)
        {
            PauseGame();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
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
        Time.timeScale = 1;
        isGameOver = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
