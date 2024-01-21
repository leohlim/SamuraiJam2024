using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
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
            Debug.Log("Victory");
            victoryScreen.SetActive(true);
        }
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
