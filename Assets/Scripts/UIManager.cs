using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Text restartText;
    [SerializeField] private Image livesImg;
    [SerializeField] private Sprite[] liveSprites;

    // Start is called before the first frame update
    void Start()
    {
        restartText.gameObject.SetActive(false);
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

    void GameOverSequence()
    {
        gameManager.GameOver();
        restartText.gameObject.SetActive(true);
    }
}
