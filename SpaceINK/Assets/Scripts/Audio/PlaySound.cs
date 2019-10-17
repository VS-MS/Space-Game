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
        Invoke("StartSound", delayTime);
    }

    private void StartSound()
    {
        FindObjectOfType<AudioManager>().Play(soundName);
    }

    public void StartSound(string sound)
    {
        FindObjectOfType<AudioManager>().Play(sound);
    }
}
