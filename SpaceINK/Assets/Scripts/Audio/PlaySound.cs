using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Старый класс, больше не используется (хотя может где-то еще остался)
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
