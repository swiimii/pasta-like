using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{

    [SerializeField] GameObject playerInteractionUI;
    [SerializeField] List<string> players;
    [SerializeField] GameObject deathScreen;
    public string gameKey = "";
    public float enemySpeedMagnitude = 0;
    public float playerAttackMagnitude = 0;
    public int levelNum = 0;
    public Text deathScreenText;

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
        deathScreen.SetActive(false);
        players.Clear();
        StartCoroutine("DeleteNetworkRoom");
        levelNum = 0;
        SceneManager.LoadScene(1);
    }

    public void PlayAgain()
    {
        deathScreen.SetActive(false);
        levelNum = 0;
        SceneManager.LoadScene(2);
    }
    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        deathScreenText.text = "You made it 'till level " + levelNum + "!";
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

    public IEnumerator DeleteNetworkRoom()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        UnityWebRequest www = UnityWebRequest.Delete("http://flask-dot-pasta-like.appspot.com/rooms/" + gameKey);
        print(gameKey);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Room Key Delete Failed!");
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Room Key Deleted!");
        }
    }

    public IEnumerator DeleteNetworkQuestion()
    {
        UnityWebRequest www = UnityWebRequest.Delete("http://flask-dot-pasta-like.appspot.com/rooms/"+ gameKey + "/active");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Question Delete Failed!");
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Question Deleted!");
        }
    }
}
