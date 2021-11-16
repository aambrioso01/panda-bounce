using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;


    public static GameManager Instance;

    public List<GameObject> playerPrefabs;

    public GameObject playerSelectPage;
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countdownPage;

    public Text scoreText;
    public Text highscore;

    enum PageState
    {
        None,
        Start,
        GameOver,
        Countdown,
        PlayerSelect
    }

    int score = 0;
    bool gameOver = true;

    public bool GameOver { get { return gameOver; } }
    public int Score { get { return score;  } }

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;
    }

    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }

    void OnCountdownFinished()  
    {
        SetPageState(PageState.None);
        OnGameStarted();
        score = 0;
        gameOver = false;
    }

    void OnPlayerDied()
    {
        gameOver = true;
        int savedScore = PlayerPrefs.GetInt("Highscore");
        if (score > savedScore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            //HighscoreText.UpdateHighscore();
        }
        SetPageState(PageState.GameOver);
    }

    void OnPlayerScored()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countdownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(true);
                break;
            case PageState.PlayerSelect:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countdownPage.SetActive(false);
                playerSelectPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        // activated when replay button is hit
        OnGameOverConfirmed();
        scoreText.text = "0";
        SetPageState(PageState.Start);
        //Debug.Log("Confirm GAMEOVER");
        //SceneManager.LoadScene("Level0");
    }

    public void StartGame()
    {
        //SetPageState(PageState.Countdown);
        SetPageState(PageState.PlayerSelect);
    }

    public void PlayerSelect(int selection)
    {
        Destroy(GameObject.FindGameObjectWithTag("PlayerGFX"));
        Instantiate(playerPrefabs[selection].gameObject, new Vector3(-0.97f, 1.7f, 0f), Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform);
        FindObjectOfType<TapController>().PlayerConfig();

        SetPageState(PageState.Countdown);
    }
}
