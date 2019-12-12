using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public bool objPool;
    //private bool objPoolFlag = false;
    [SerializeField]
    private string soundName;
    [SerializeField]
    private float delayTime;

    private void OnEnable()   
    {
        if (!objPool)
        {
            if (!String.IsNullOrEmpty(soundName))//Временная заглушка, для того, чтобы звук не проигрывался когда наполняется пул объектов.
            {
                Debug.Log(this);
                Invoke("StartSound", delayTime);
            }
        }
        else objPool = false;

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
