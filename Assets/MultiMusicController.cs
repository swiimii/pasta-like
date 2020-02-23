using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MultiMusicController : MonoBehaviour
{
    public AudioSource combatMusic, idleMusic, voteMusic;
    
    public void PlayCombatMusic()
    {
        StopAllMusic();
        combatMusic.Play();
    }

    public void PlayIdleMusic()
    {
        StopAllMusic();
        idleMusic.Play();
    }

    public void PlayVoteMusic()
    {
        StopAllMusic();
        voteMusic.Play();
    }

    public void StopAllMusic()
    {
        if (combatMusic.isPlaying)
        {
            combatMusic.Stop();
        }
        if (idleMusic.isPlaying)
        {
            idleMusic.Stop();
        }
        if (voteMusic.isPlaying)
        {
            voteMusic.Stop();
        }
    }
    
}
