using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public GameObject deathScreen;

    private void Update()
    {
        // If player presses R at any time, restart the game
        if(Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Reload pressed");
                Restart();
            }
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game over man!");
            deathScreen.SetActive(true);
        }
    }

    void Restart ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
