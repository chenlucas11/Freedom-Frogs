using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameOver;

    // Music Pieces
    [SerializeField] private int piecesCollected;

    // Start is called before the first frame update
    void Start()
    {
        piecesCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); // Current Game Scene
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void PieceCollected()
    {
        piecesCollected++;
    }
}
