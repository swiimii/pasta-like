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
    public Text rightCountUI;
    public Text leftCountUI;

    public int countdownMaximum = 20;
    public int postCountdownTime = 2;

    public string left = "1";
    public string right = "2";

    public int leftCount = 0;
    public int rightCount = 0;

    private string questionId;

    public LockedRoom room;

    private void OnEnable()
    {
        StartCoroutine("PlayerInteraction");
        StartCoroutine("PostQuestions");
        // StartCoroutine("GetResponses");
    }

    public IEnumerator PlayerInteraction()
    {

        int timer = countdownMaximum;
        countdownTimer.text = timer.ToString();
        while (timer >= 0)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
            countdownTimer.text = timer.ToString();
            rightCountUI.text = rightCount.ToString();
            leftCountUI.text = leftCount.ToString();
        }

        if (leftCount > rightCount)
        {
            option1.GetComponent<ScalingOptionScript>().ResolveOption();
            option2.GetComponentInChildren<Image>().color = new Color(1, 1, 1, .5f);
        }
        else if (rightCount > leftCount)
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

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().StartCoroutine("DeleteNetworkQuestion");

        room.Unlock();
        gameObject.SetActive(false);
    }

    public IEnumerator PostQuestions()
    {        
        var gameKey = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().gameKey;

        if (gameKey.Equals(""))
        {
            yield return null;
        }
        else
        {
            var input = new OptionPost(left, right);
            UnityWebRequest www = UnityWebRequest.Post("https://flask-dot-pasta-like.appspot.com/questions/" + gameKey, JsonUtility.ToJson(input));
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Question Post Failed!");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Question Post Success!");
                questionId = www.downloadHandler.text;
                print(questionId);
                
            }
        }

        string url = "https://flask-dot-pasta-like.appspot.com/responses/" + questionId.Substring(1,questionId.Length-3);
        while (true)
        {
            
            print(url);
            UnityWebRequest www = UnityWebRequest.Get(url);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Response Get Failed!");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Response Get Success!");
                if (!www.downloadHandler.text.Trim().Equals("{}"))
                {
                    print(www.downloadHandler.text);
                    // var output = JsonUtility.FromJson<Dictionary<string, string>>(www.downloadHandler.text.Trim());
                    var myString = www.downloadHandler.text;

                    string[] arr = myString.Split(',');

                    leftCount = 0;
                    rightCount = 0;
                    foreach (string str in arr)
                    {
                        string[] vals = str.Split(':');
                        if (vals.Length != 2)
                        {
                            continue;
                        }
                        string value = vals[1].Substring(1, 1);
                        print(value);
                        if (value.Equals(left))
                        {
                            leftCount++;
                        }
                        if (value.Equals(right))
                        {
                            rightCount++;
                        }
                    }
                    
                }
            }           
            yield return new WaitForSeconds(.3f);
        }

    }

    //public IEnumerator GetResponses()
    //{
    //    string url = "https://flask-dot-pasta-like.appspot.com/responses/" + questionId;
    //    while (true)
    //    {        
    //        var gameKey = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().gameKey;

    //        if (gameKey.Equals(""))
    //        {
    //            yield return null;
    //        }
    //        else
    //        {
    //            print(url);
    //            UnityWebRequest www = UnityWebRequest.Get(url);

    //            yield return www.SendWebRequest();

    //            if (www.isNetworkError || www.isHttpError)
    //            {
    //                Debug.Log("Response Get Failed!");
    //                Debug.Log(www.error);
    //            }
    //            else
    //            {
    //                Debug.Log("Response Get Success!");
    //                print(www.downloadHandler.text);
    //                var myString = www.downloadHandler.text;
                    
    //                string[] arr = myString.Split(',');
    //                foreach (string str in arr)
    //                {
    //                    string[] vals = str.Split(':');
    //                    if (vals.Length != 2)
    //                    {
    //                        continue;
    //                    }
    //                    string value = vals[1].Substring(1, 2);
    //                    print(value);
    //                    if (value.Equals(left))
    //                    {
    //                        leftCount++;
    //                    }
    //                    if (value.Equals(right))
    //                    {
    //                        rightCount++;
    //                    }
    //                }
    //                //var output = JsonUtility.FromJson<ResponseGet>(www.downloadHandler.text);
    //                var output = JsonUtility.FromJson<ResponseGet>(www.downloadHandler.text);
    //                foreach(string value in output.answerMap.Values)
    //                {
    //                    print("Value: " + value);
    //                    if(value.Equals(left))
    //                    {
    //                        leftCount++;
    //                    }
    //                    if(value.Equals(right))
    //                    {
    //                        rightCount++;
    //                    }
    //                }
    //            }
    //        }

    //        yield return new WaitForSeconds(.3f);
    //    }
    //}

    class OptionPost
    {
        public List<string> opt;
        public OptionPost(string s1, string s2)
        {
            opt = new List<string>();
            opt.Add(s1);
            opt.Add(s2);
        }
    }
    class ResponseGet
    {
        public Dictionary<string, string> answerMap;
    }
    private void OnDisable()
    {
        leftCount = 0;
        rightCount = 0;
        leftCountUI.text = "";
        rightCountUI.text = "";
    }

}
