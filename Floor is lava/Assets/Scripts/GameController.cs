using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static bool isGameOver;
    static string currentScene;
    static ContinuousMovement movement;
    public static Canvas gameOverCanvas;
    public static Canvas levelCompleteCanvas;
    public static bool gameIsRunning;
    public static bool gameJustStarted;
    public static bool isLevelCompleted;
    TimerController timer;
    public static AudioManager audio;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        movement = FindObjectOfType<ContinuousMovement>();
        timer = GetComponent<TimerController>();
        gameIsRunning = true;
        gameJustStarted = true;
        isGameOver = false;
        isLevelCompleted = false;
    }

    private void FixedUpdate()
    {
        if (!isGameOver)
        {
            gameOverCanvas.gameObject.SetActive(false);
            levelCompleteCanvas.gameObject.SetActive(false);
        }

        //Debug.Log(gameJustStarted);
        StartCoroutine("DisableGameJustStarted");

        if (gameIsRunning)
        {
            timer.BeginTimer();
        }
    }

    public static void GameOver()
    {
        if (!isGameOver)
        {
            if (gameJustStarted) return;

            movement.canMove = false;
            if (!isLevelCompleted)
            {
                gameOverCanvas.gameObject.SetActive(true);
            }
            else
            {
                levelCompleteCanvas.gameObject.SetActive(true);
            }
            gameIsRunning = false;
            isGameOver = true;
        }
    }

    public static void RestartGame()
    {
        if (isGameOver)
        {
            SceneManager.LoadScene(currentScene);
            //Debug.Log("Restarted");
            isGameOver = false;
            isLevelCompleted = false;
        }
    }

    public static void QuitGame()
    {

        //If we are running in the editor
        #if UNITY_EDITOR
            //Stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }


    IEnumerator DisableGameJustStarted()
    {
        yield return new WaitForSeconds(0.01f);
        if (gameJustStarted)
            gameJustStarted = false;

    }
}

