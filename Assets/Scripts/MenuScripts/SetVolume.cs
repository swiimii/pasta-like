using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public string volumeType;
    public AudioMixer mixer;
    public Slider slider;

    public void SetLevel()
    {
        mixer.SetFloat(volumeType, Mathf.Log10(slider.value) * 20);
    }


}
