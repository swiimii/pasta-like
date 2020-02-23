using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] List<string> players;

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
}
