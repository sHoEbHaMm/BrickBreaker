using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    [SerializeField] private int Score = 0;
    [SerializeField] private int Lives = 3;
    [SerializeField] private int Level = 1;
    private Paddle Paddle;
    private Ball Ball;
    private Brick[] Bricks;

    [SerializeField] TMP_Text scoreNumber;
    [SerializeField] TMP_Text livesNumber;
    [SerializeField] Image gameOverScreen;
    [SerializeField] Image levelCompletedScreen;

    private void Awake()
    {
        /* This will avoid the gamemanager object from destroying when we load & unload scenes as per our need */
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        scoreNumber.text = Score.ToString();
        livesNumber.text = Lives.ToString();
    }

    /* Game Initialization */
    private void StartGame()
    {
        this.Score = 0;
        this.Lives = 3;
        LoadLevel(1);
    }


    /* Handles loading of the levels. This function presumes that the naming convention of our levels is in the following manner
     * "StringInteger" - for example - Level1. The function concatenates "Level" with the function parameter and loads the scene. */
    private void LoadLevel(int _level)
    {
        this.Level = _level;
        SceneManager.LoadScene("Level" + _level);
    }

    /* Handles the score increment */
    public void IncreaseScore(Brick brick)
    {
        this.Score += brick.Points;

        if(isLevelCompleted())
        {
            levelCompletedScreen.gameObject.SetActive(true); // Loads the level completed UI
            Invoke(nameof(StartGame), 2f); // Waits for 2 secs before restarting the game
        }
    }

    /* Handles the decrement of lives and resetting the ball and paddle after every live lost */
    public void ReduceLives()
    {
        this.Lives--;

        if (Lives <= 0) 
        {
            gameOverScreen.gameObject.SetActive(true); // Loads the game over UI
            Invoke(nameof(StartGame), 2f); // Waits for 2 secs before restarting the game
        }
        else
        {
            this.Paddle.ResetPaddle();
            this.Ball.ResetBall();
        }
    }

    /* Get the paddle, ball and bricks from active scene */
    public void OnSceneLoaded(Scene activeScene, LoadSceneMode mode)
    {
        Paddle = FindObjectOfType<Paddle>();
        Ball = FindObjectOfType<Ball>();
        Bricks = FindObjectsOfType<Brick>();
    }

    /* Checks if all the bricks in the level are destroyed. If yes, returnss true, else returns false */
    public bool isLevelCompleted()
    {
        if (Bricks.Length <= 0)
            return true;
        return false;
    }

}
