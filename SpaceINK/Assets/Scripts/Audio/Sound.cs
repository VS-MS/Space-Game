using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound
{
    //Имя звука, используется как индивидуальный ключ для поиска в списке
    public string name;
    //Ссылка на сам клип
    public AudioClip clip;

    //Уровень громкости
    [Range(0f, 1f)]
    public float volume;
    //Скорость воспроизведения
    [HideInInspector]
    [Range(.1f, 3f)]
    public float pitch;

    //Повтор
    public bool loop;

    //Ссылка на источник звука
    [HideInInspector]
    public AudioSource source;

}
