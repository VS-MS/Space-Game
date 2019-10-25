using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    /*
     * Для запуска из любого скрипта, можно использовать такую конструкцию
     * FindObjectOfType<AudioManager>().Play("NazvanieSound");
     */

    public bool audioVFX;
    public bool audioMusic;
    public Sound[] sounds;
    public static AudioManager instance;
    void Awake()
    { 
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        

    }

    public void Play(string name)
    {
        if (audioVFX)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.Log("sound not found " + name);
                return;
            }
            s.source.Play();
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound not found " + name);
            return;
        }
        if (String.Equals(s.name, "MenuTheme"))
        {
            if(!s.source.isPlaying)
            {
                s.source.Play();
            }

        }
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound not found " + name);
            return;
        }
        s.source.Stop();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("sound not found " + name);
            return;
        }
        s.source.Pause();

    }

}

