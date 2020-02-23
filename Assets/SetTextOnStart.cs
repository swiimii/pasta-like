using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetTextOnStart : MonoBehaviour
{
    public Text target;
    private void Start()
    {
        target.text = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>().gameKey;
    }
    
}
