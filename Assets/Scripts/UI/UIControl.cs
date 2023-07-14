using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }

    #region Game Over
    //���������� ���� ��������� ����
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundControl.instance.PlaySound(gameOverSound);
        //resetScore = PlayerPrefs.GetFloat("score");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   //������ �� ������� �����
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);                            //� ������� ����
    }

    public void Quit()
    {
        Application.Quit();                                   //����� �� ���� � ������� ������

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;      //����� �� ���� ������ ��������� Unity
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
        SoundControl.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundControl.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
}
