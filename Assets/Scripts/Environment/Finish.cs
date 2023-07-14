using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioClip finishSound;

    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero" && !levelCompleted)
        {
            SoundControl.instance.PlaySound(finishSound);
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);                    //обождать перед запуском
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
