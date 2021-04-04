using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private Image musicImg;
    [SerializeField] private Sprite[] musicSprites;
    private bool musicOn = true;

    // Start is called before the first frame update
    void Start()
    {
        musicOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameAudio.isPlaying)
        {
            gameAudio.clip = audioClips[1];
            gameAudio.Play();
            gameAudio.loop = true;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1); // Current Opening Scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MusicToggle()
    {
        if (musicOn)
        {
            musicImg.sprite = musicSprites[0];
            gameAudio.volume = 0f;
            musicOn = false;
        }
        else
        {
            musicImg.sprite = musicSprites[1];
            gameAudio.volume = 0.35f;
            musicOn = true;
        } 
    }
}
