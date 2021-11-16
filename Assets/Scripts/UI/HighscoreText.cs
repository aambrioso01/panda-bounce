using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreText : MonoBehaviour
{
    Text highscore;

    void Start()
    {
        highscore = GetComponent<Text>();
        highscore.text = "HighScore: " + PlayerPrefs.GetInt("Highscore").ToString();
    }

    void Update()
    {     
        highscore.text = "HighScore: " + PlayerPrefs.GetInt("Highscore").ToString();
    }
}
