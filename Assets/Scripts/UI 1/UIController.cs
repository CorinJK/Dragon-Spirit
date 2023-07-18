using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    private float resetScore = 0;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    #region Game Over
    //активирует окно окончания игры
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundController.instance.PlaySound(gameOverSound);
        //resetScore = PlayerPrefs.GetFloat("score");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //вернет на текущую сцену
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);                            //в главное меню
    }

    public void Quit()
    {
        Application.Quit();                                   //выйти из игры в готовой сборке

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;      //выйти из игры внутри редактора Unity
        #endif
    }
    #endregion

    #region Pause
    public void SetPause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseOff()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void SoundVolume()
    {
        SoundController.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundController.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}
