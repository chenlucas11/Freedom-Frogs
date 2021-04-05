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
        if (Input.GetKeyDown(KeyCode.Return))
            NextSlide();
    }

    public void Skip()
    {
        SceneManager.LoadScene(2); // Current Game Scene
    }

    public void LastSlide()
    {
        if (slideNum - 1 >= 0)
        {
            slideNum--;
            sceneImg.sprite = sceneSprites[slideNum];
        }
        else
            SceneManager.LoadScene(0); // Main Menu
    }

    public void NextSlide()
    {
        if (slideNum + 1 >= sceneSprites.Length)
            Skip();
        else
        {
            slideNum++;
            sceneImg.sprite = sceneSprites[slideNum];
        }
    }
}
