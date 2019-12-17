using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public bool objPool;//Переменная обозначающая, что звук назодится в пуле объектов.
    //private bool objPoolFlag = false;
    [SerializeField]
    private string soundName;
    [SerializeField]
    private float delayTime;

    private void OnEnable()   
    {
        //Временная заглушка, для того, чтобы звук не проигрывался когда наполняется пул объектов.
        //Если в инспекторе поставить галочку objPool, то данный объект будем принадлежать пулу
        //И при первом создании для добавдения в пул, звук не будет проигран
        if (!objPool)
        {
            if (!String.IsNullOrEmpty(soundName))
            {
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
