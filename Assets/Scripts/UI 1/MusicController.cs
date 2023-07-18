using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;

    //сохраняем объект при переходе между сценами
    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
