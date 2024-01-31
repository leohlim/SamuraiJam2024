using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    bool victory = false;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public static bool isPaused = false;
    public GameObject pauseMenuScreen;
    public GameObject player;
    public GameObject camera;
    public Vector3 startPosition;
    public GameObject ost;



    private void Start()
    {
        startPosition = player.transform.position;
        

    }

    private void Update()
    {
        // If player presses R at any time, restart the game
        if(Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Reload pressed");
                Restart();
            }
            
        if(Input.GetKeyDown(KeyCode.Space) && victory == true)
                {
                    FindObjectOfType<GlobalSound>().Unmute();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }

        if(Input.GetKeyDown(KeyCode.Escape) && gameHasEnded == false)
        {
            if(isPaused)
            {
                Resume();
            } else {
                Pause();
            }
        }
        
    }

    public void GameOver()
    {
        if (!gameHasEnded)
        {

            player.SetActive(false);
            gameHasEnded = true;
            Debug.Log("Game over man!");
            FindObjectOfType<GlobalSound>().Mute();
            gameOverScreen.SetActive(true);
            
        }
    }

    public void Victory()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            victory = true;
            Debug.Log("Victory");
            FindObjectOfType<GlobalSound>().Mute();
            victoryScreen.SetActive(true);
            
        }
    }

    void Restart ()
    {
        gameHasEnded = false;
        victory = false;
        victoryScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        player.transform.position = startPosition;
        player.SetActive(true);
        FindObjectOfType<SamuraiCollision>().PlayerInputEnabled();
        ost.SetActive(true);
        FindObjectOfType<GlobalSound>().Unmute();
        FindObjectOfType<Stopwatch>().restartTimer();



    }

    void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        player.SetActive(true);
        camera.GetComponentInChildren<SamuraiLook>().enabled = true;
        FindObjectOfType<GlobalSound>().Unmute();


    }

    void Pause()
    {        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        player.SetActive(false);
        camera.GetComponentInChildren<SamuraiLook>().enabled = false;
        FindObjectOfType<GlobalSound>().Mute();

    }
}
