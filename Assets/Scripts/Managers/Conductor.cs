using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public float bpm = 120;
    public float timePerBeat;
    public float songPosition = 0;
    public float startSongPosition;
    public float offset = 0f;
    public float lastBeat;

    public int beatsPerBar = 4;
    public int beatNum;
    public int barNum;


    // Start is called before the first frame update
    void Start()
    {
        lastBeat = 0;
        timePerBeat = 60 / bpm;
        beatNum = 0;
        barNum = 0;
        startSongPosition = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - startSongPosition) - offset;
        if (songPosition > lastBeat + timePerBeat)
        {
            lastBeat += timePerBeat;
            beatNum++;
            if(beatNum > 4)
            {
                barNum++;
            }
            beatNum %= beatsPerBar;
        }
    }

    public void StartSong()
    {
        startSongPosition = (float)AudioSettings.dspTime;
        lastBeat = 0;
        timePerBeat = 60 / bpm;
        beatNum = 0;
        barNum = 0;
    }
}
