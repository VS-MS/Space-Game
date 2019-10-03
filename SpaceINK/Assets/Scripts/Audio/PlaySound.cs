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
        Debug.Log("piu");
        FindObjectOfType<AudioManager>().Play(soundName);
    }
}
