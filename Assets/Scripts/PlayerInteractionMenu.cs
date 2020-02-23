using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionMenu : MonoBehaviour
{
    public GameObject option1;
    public GameObject option2;
    public Text countdownTimer;

    public int countdownMaximum = 20;
    public int postCountdownTime = 2;

    private void OnEnable()
    {
        StartCoroutine("PlayerInteraction");
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
        gameObject.SetActive(false);


    }

}
