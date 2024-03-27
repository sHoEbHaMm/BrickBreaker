using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    [SerializeField] private int Score = 0;
    [SerializeField] private int Lives = 3;
    [SerializeField] private int Level = 1;

    private void Awake()
    {
        /* This will avoid the gamemanager object from destroying when we load & unload scenes as per our need */
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
}
