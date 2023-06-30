using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayCurrentLevel1()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenLevelsList() 
    {
        SceneManager.LoadScene(1);
    }
}