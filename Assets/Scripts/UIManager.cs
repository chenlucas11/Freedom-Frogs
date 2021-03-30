using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Text restartText;

    // Start is called before the first frame update
    void Start()
    {
        restartText.gameObject.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateLives(int currentLives)
    {
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
