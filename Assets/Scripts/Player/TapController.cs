using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    public float tapForce = 10;
    public float tiltSmooth = 5;
    public Vector3 startPos;

    Rigidbody2D rb;
    Quaternion downRotation;
    Quaternion forwardRotation;

    GameManager game;

    bool GameIsPaused;

    void Awake()
    {
        game = GameManager.Instance;
    }

    void Start()
    {
        PlayerConfig();
    }

    public void PlayerConfig()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
        downRotation = Quaternion.Euler(0, 0, -90);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisabled()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        PlayerConfig();
        rb.velocity = Vector3.zero;
        rb.simulated = true;
        Time.timeScale = 1f;
    }

    void OnGameOverConfirmed()
    {
        transform.GetChild(0).gameObject.transform.position = startPos;
        transform.GetChild(0).gameObject.transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        if (game.GameOver) return;

        // player jump on tap
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            //Time.timeScale = Time.timeScale * 1.01f;
            transform.GetChild(0).gameObject.transform.rotation = forwardRotation;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * tapForce, ForceMode2D.Force);

            // play jump sound
            FindObjectOfType<AudioManager>().Play("PlayerBounce");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("DELETE");
            PlayerPrefs.SetInt("Highscore", 0);
        }

        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        transform.GetChild(0).gameObject.transform.rotation = Quaternion.Lerp(transform.GetChild(0).gameObject.transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("COLLISION: " + col);
        if (col.gameObject.tag == "ScoreZone")
        {
            // event sent to GameManager
            OnPlayerScored();
        }

        // death
        if (col.gameObject.tag == "DeadZone" || col.gameObject.tag == "GroundDeath")
        {
            rb.simulated = false;
            // event sent to GameManager
            OnPlayerDied();

            // play death sound
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
        }
    }
}
