using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSourse;
    private AudioSource musicSourse;

    private void Awake()
    {
        instance = this;        //������� ��������
        soundSourse = GetComponent<AudioSource>();
        musicSourse = transform.GetChild(0).GetComponent<AudioSource>();

        //����� ������� ������ �� ���������� ������ ��� ����� ������
        if (instance == null)   //�������� ������� ������� ������ �� ������
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //����������� ���������
        else if (instance != null && instance != this)
            Destroy(gameObject);

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSourse.PlayOneShot(_sound);     //���������� ������ ���� ���
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
        //����� ��������� ��������� �������� ��������� � �������� ���
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);      //��������� ��������� ���������
        currentVolume += change;

        //�������� �� ����������� � ������������ ��������
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        //����������� �������� ��������
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        //��������� �������� ��������� 
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
