using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour
{
    [SerializeField] private Image sceneImg;
    [SerializeField] private Sprite[] sceneSprites;
    private int slideNum;

    // Start is called before the first frame update
    void Start()
    {
        slideNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            NextSlide();
    }

    public void Skip()
    {
        SceneManager.LoadScene(2); // Current Game Scene
    }

    public void NextSlide()
    {
        slideNum++;
        if (slideNum >= sceneSprites.Length)
            Skip();
        else
            sceneImg.sprite = sceneSprites[slideNum];
    }
}
