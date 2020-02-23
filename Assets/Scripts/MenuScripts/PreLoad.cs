using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreLoad : MonoBehaviour
{    
    void Update()
    {
        SceneManager.LoadScene(1);
    }
}
