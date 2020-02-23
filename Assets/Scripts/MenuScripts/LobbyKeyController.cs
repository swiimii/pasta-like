using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LobbyKeyController : MonoBehaviour
{
    public Text gameKey;

    private void OnEnable()
    {
        StartCoroutine("GetLobbyKey");
    }

    public IEnumerator GetLobbyKey()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();        

        UnityWebRequest www = UnityWebRequest.Post("http://flask-dot-pasta-like.appspot.com/rooms/", "");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Room Key Get Failed!");
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Room Key Acquired!");
            print(www.downloadHandler.text);
            gameKey.text = www.downloadHandler.text.Substring(1, 4);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().gameKey = www.downloadHandler.text.Substring(1, 4);
        }
    }

    private void OnDisable()
    {
        gameKey.text = "...";
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().StartCoroutine("DeleteNetworkRoom");

    }
}
