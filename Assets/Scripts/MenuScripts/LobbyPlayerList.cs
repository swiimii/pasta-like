using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyPlayerList : MonoBehaviour
{
    [SerializeField] Text playerList;
    int maxPlayers = 4;
    [SerializeField] float interval = 5, currentTime;

    private void Update()
    {
        currentTime -= Time.deltaTime;
        if(currentTime < 0)
        {
            currentTime = interval;
            StartCoroutine("CheckServerPlayerList");
        }
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

    public IEnumerator CheckServerPlayerList()
    {
        var gameKey = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().gameKey;

        if (gameKey.Equals(""))
        {
            yield return null;
        }
        else
        {
            UnityWebRequest www = UnityWebRequest.Get("http://flask-dot-pasta-like.appspot.com/rooms/" + gameKey);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Player Get Failed!");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Player Get Success!");
                print(www.downloadHandler.text);
                PlayerList requestOut = new PlayerList();
                requestOut = JsonUtility.FromJson<PlayerList>(www.downloadHandler.text);
                playerList.text = "";
                foreach(string str in requestOut.players)
                {
                    playerList.text += str + "\n";
                }
                print(www.downloadHandler.text);
            }
        }
    }

    public class PlayerList
    {
        public int max_players;
        public string[] players;
        public string room_id;
        public string room_status;
        public string room_type;
        public float time_start;
    }

}
