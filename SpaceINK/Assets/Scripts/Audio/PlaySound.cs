using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    [SerializeField]
    private string soundName;
    [SerializeField]
    private float delayTime;
    private void Awake()
    {
        
        if (!String.IsNullOrEmpty(soundName))
        {
            Invoke("StartSound", delayTime);
        }
        
    }

    private void StartSound()
    {
        FindObjectOfType<AudioManager>().Play(soundName);
        
    }

    public void StartSound(string sound)
    {
        FindObjectOfType<AudioManager>().Play(sound);
    }

    public void StopSound(string sound)
    {
        FindObjectOfType<AudioManager>().Stop(sound);
    }
}
