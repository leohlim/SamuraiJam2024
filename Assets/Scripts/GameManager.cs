using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    bool victory = false;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;


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
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
        
    }

    public void GameOver()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game over man!");
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
            victoryScreen.SetActive(true);
            
        }
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
