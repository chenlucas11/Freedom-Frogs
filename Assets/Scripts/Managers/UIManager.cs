using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Text restartText;
    [SerializeField] private Text finishText;
    [SerializeField] private Image livesImg;
    [SerializeField] private Sprite[] liveSprites;
    [SerializeField] private Image musicPieceImg;
    [SerializeField] private Sprite[] musicPieceSprites;
    [SerializeField] private Image leftArrowImg;
    [SerializeField] private Image middleArrowImg;
    [SerializeField] private Image rightArrowImg;
    [SerializeField] private Sprite[] arrowSprites;

    // Start is called before the first frame update
    void Start()
    {
        restartText.gameObject.SetActive(false);
        finishText.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateLives(int currentLives)
    {
        livesImg.sprite = liveSprites[currentLives];
        if (currentLives < 1)
        {
            GameOverSequence();
        }
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

    void GameOverSequence()
    {
        gameManager.GameOver();
        restartText.gameObject.SetActive(true);
    }

    void FinishGameSequence()
    {
        finishText.gameObject.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;

    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
