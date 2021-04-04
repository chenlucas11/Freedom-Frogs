using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip[] audioClips;
    private Conductor conductor;

    // Start is called before the first frame update
    void Start()
    {
        conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
        gameAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAudio(int piecesCollected)
    {
        if(piecesCollected > 0 && piecesCollected < 4)
        {
            gameAudio.clip = audioClips[piecesCollected - 1];
            gameAudio.Play();
            conductor.StartSong();
        }
        else if(piecesCollected == 4)
        {
            gameAudio.clip = audioClips[piecesCollected - 1];
            gameAudio.Play();
            gameAudio.loop = false;
        }
        // GameOver Audio
        else if (piecesCollected == 5)
        {
            gameAudio.clip = audioClips[piecesCollected - 1];
            gameAudio.Play();
        }
    }
}
