using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyPlayerList : MonoBehaviour
{
    [SerializeField] Text playerList;
    int maxPlayers = 4;

    private void OnEnable()
    {
        StartCoroutine("MockPlayersJoining");
    }

    public void AddPlayerToList(string playerName)
    {
        var state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        if (state.PlayerCount() <= maxPlayers)
        {
            
            if (!playerList.text.Equals(""))
            {
                playerList.text += "\n";
            }
            playerList.text += playerName;
            state.AddPlayer(playerName);
        }
    }

    public void ResetPlayerList()
    {
        playerList.text = "";
        var state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        state.ResetPlayers();

    }

    public IEnumerator MockPlayersJoining()
    {
        while(true)
        {
            AddPlayerToList(name);
            yield return new WaitForSeconds(4);
        }
    }

}
