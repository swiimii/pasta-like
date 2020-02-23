using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{

    [SerializeField] GameObject playerInteractionUI;
    [SerializeField] List<string> players;
    [SerializeField] GameObject deathScreen;

    private void Start()
    {
        if(GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayer(string playerName)
    {
        players.Add(playerName);
    }

    public void ResetPlayers()
    {
        players.Clear();
    }

    public int PlayerCount()
    {
        return players.Count;
    }

    public GameObject ShowPlayerInteractionUI()
    {
        playerInteractionUI.SetActive(true);
        return playerInteractionUI;
    }

    public void ReturnToMenu()
    {
        players.Clear();
        SceneManager.LoadScene(1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(2);
    }
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
    }
    public void TogglePauseMenu()
    {
        if(deathScreen.activeSelf)
        {
            TogglePauseMenu(false);
        }
        else
        {
            TogglePauseMenu(true);
        }        
    }

    public void TogglePauseMenu(bool input)
    {
        deathScreen.SetActive(input);
    }
}
