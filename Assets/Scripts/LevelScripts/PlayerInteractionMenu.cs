using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class PlayerInteractionMenu : MonoBehaviour
{
    public GameObject option1;
    public GameObject option2;
    public Text countdownTimer;

    public int countdownMaximum = 20;
    public int postCountdownTime = 2;

    public LockedRoom room;

    private void OnEnable()
    {
        StartCoroutine("PlayerInteraction");
        StartCoroutine("PostQuestions");
    }

    public IEnumerator PlayerInteraction()
    {
        int votes1 = 0;
        int votes2 = 1;

        int timer = countdownMaximum;
        countdownTimer.text = timer.ToString();
        while (timer >= 0)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
            countdownTimer.text = timer.ToString();
        }

        if (votes1 > votes2)
        {
            option1.GetComponent<ScalingOptionScript>().ResolveOption();
            option2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, .5f);
        }
        else if (votes2 > votes1)
        {
            option2.GetComponent<ScalingOptionScript>().ResolveOption();
            option1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, .5f);
        }
        else
        {
            option2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, .5f);
            option1.GetComponentInChildren<Image>().color = new Color(1, 1, 1, .5f);
        }
        // post-selection countdown
        timer = postCountdownTime;
        countdownTimer.text = timer.ToString();
        while (timer >= 0)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
            countdownTimer.text = timer.ToString();
        }

        option2.GetComponentInChildren<Image>().color = Color.white;
        option1.GetComponentInChildren<Image>().color = Color.white;

        room.Unlock();
        gameObject.SetActive(false);
    }

    public IEnumerator PostQuestions()
    {
        var desc1 = option1.GetComponent<ScalingOptionScript>().option.description;
        var desc2 = option2.GetComponent<ScalingOptionScript>().option.description;

        var gameKey = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().gameKey;

        if (gameKey.Equals(""))
        {
            yield return null;
        }
        else
        {
            var input = new OptionPost(desc1, desc2);
            UnityWebRequest www = UnityWebRequest.Post("http://flask-dot-pasta-like.appspot.com/questions/" + gameKey, JsonUtility.ToJson(input));

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Question Post Failed!");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Question Post Success!");
                print(www.downloadHandler.text);                
            }
        }
        
    }

    class OptionPost
    {
        public List<string> opt;
        public OptionPost(string s1, string s2)
        {
            opt.Add(s1);
            opt.Add(s2);
        }
    }

}
