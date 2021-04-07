using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;
    [SerializeField] private GameObject finishScene;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private Image livesImg;
    [SerializeField] private Sprite[] liveSprites;
    [SerializeField] private Image musicPieceImg;
    [SerializeField] private Sprite[] musicPieceSprites;
    [SerializeField] private Image leftArrowImg;
    [SerializeField] private Image middleArrowImg;
    [SerializeField] private Image rightArrowImg;
    [SerializeField] private Sprite[] arrowSprites;
    [SerializeField] private GameObject tutorialImg;
    private bool gameFinished = false;
    private bool tutorialOn = false;

    // Start is called before the first frame update
    void Start()
    {
        finishScene.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        tutorialImg.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (gameFinished && !audioManager.IsPlaying())
        {
            gameManager.MainMenu();
        }
        if(Input.GetKeyDown(KeyCode.Return) && tutorialOn)
        {
            UpdateTutorial();
        }
    }

    public void UpdateLives(int currentLives)
    {
        livesImg.sprite = liveSprites[currentLives];
        /*if (currentLives < 1)
        {
            //GameOverSequence();
        }*/
    }

    public void UpdateArrows(int num, int dir)
    {
        switch (num)
        {
            case 0:
                leftArrowImg.sprite = arrowSprites[dir];
                middleArrowImg.sprite = arrowSprites[dir];
                rightArrowImg.sprite = arrowSprites[dir];
                break;
            case 1:
                leftArrowImg.sprite = arrowSprites[dir];
                break;
            case 2:
                middleArrowImg.sprite = arrowSprites[dir];
                break;
            case 3:
                rightArrowImg.sprite = arrowSprites[dir];
                break;
        }
    }

    public void UpdateMusicPieces(int piecesCollected)
    {
        musicPieceImg.sprite = musicPieceSprites[piecesCollected];
        if (piecesCollected == 4)
        {
            FinishGameSequence();
        }
    }

    public void UpdatePauseUI(bool isGamePaused)
    {
        if (isGamePaused)
        {
            pauseMenu.gameObject.SetActive(true);
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
        }
    }

    private void GameOverSequence()
    {
        gameManager.GameOver();
        audioManager.UpdateAudio(5);
        gameOverMenu.gameObject.SetActive(true);
    }

    private void FinishGameSequence()
    {
        finishScene.gameObject.SetActive(true);
        gameFinished = true;
    }

    public void UpdateTutorial()
    {
        if (!tutorialOn)
        {
            tutorialImg.gameObject.SetActive(true);
            tutorialOn = true;
        }
        else
        {
            tutorialImg.gameObject.SetActive(false);
            tutorialOn = false;
        }
    }
}
