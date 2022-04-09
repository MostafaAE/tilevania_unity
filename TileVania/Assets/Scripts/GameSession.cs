using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public int playerLives = 3;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if(numGameSessions > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
    }
    void Update()
    {
        
    }

    public void ProcessPlayerDeath()
    { 
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }

    }

    private void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    
}
