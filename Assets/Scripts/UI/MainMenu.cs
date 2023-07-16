using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject MainMenuScreen;
    [SerializeField] private GameObject SettingsMenuScreen;

    private Action closeAction;

    private void Start()
    {
        animator.SetTrigger("Show");
    }

    public void OnStartGame()
    {
        closeAction = () => { SceneManager.LoadScene(2); };
        Close();
    }

    public void OpenLevelsList() 
    {
        closeAction = () => { SceneManager.LoadScene(1); };
        Close();
    }

    public void OnShowSettings()
    {
        closeAction = () => 
        {
            MainMenuScreen.SetActive(false);
            SettingsMenuScreen.SetActive(true);
        };
        Close();
    }

    public void BackToMenu()
    {
        SettingsMenuScreen.SetActive(false);
        MainMenuScreen.SetActive(true);
        animator.SetTrigger("Show");
    }

    public void OnExit()
    {
        closeAction = () =>
        {
            Application.Quit();                                   //выйти из игры в готовой сборке

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;      //выйти из игры внутри редактора Unity
#endif
        };
        Close();
    }

    public void Close()
    {
        animator.SetTrigger("Hide");
    }

    public void OnCloseAnimationComplete()
    {
        closeAction?.Invoke();
    }
}