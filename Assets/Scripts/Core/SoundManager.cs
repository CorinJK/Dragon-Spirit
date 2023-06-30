using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSourse;
    private AudioSource musicSourse;

    private void Awake()
    {
        instance = this;        //паттерн синглтон
        soundSourse = GetComponent<AudioSource>();
        musicSourse = transform.GetChild(0).GetComponent<AudioSource>();

        //чтобы фоновая музыка не начиналась заново при смене уровня
        if (instance == null)   //проверка начичия фоновой музыки на уровне
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //уничтожение дубликата
        else if (instance != null && instance != this)
            Destroy(gameObject);

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSourse.PlayOneShot(_sound);     //произвести только один раз
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourseVolume(1, "soundVolume", _change, soundSourse);
    }

    public void ChangeMusicVolume(float _change)
    {
        ChangeSourseVolume(0.2f, "musicVolume", _change, musicSourse);
    }

    private void ChangeSourseVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        //берем начальное начальное значение громкости и изменяем его
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);      //сохранить последнее изменение
        currentVolume += change;

        //проверка на минимальное и максимальное значение
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //присваиваем итоговое значение
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //сохраняем конечную громкость 
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
