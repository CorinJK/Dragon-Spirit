using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score;
    [SerializeField] Text scoreText;
    [SerializeField] private AudioClip scoreSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Score")
        {
            Destroy(collision.gameObject);
            SoundManager.instance.PlaySound(scoreSound);
            score++;
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        //score = PlayerPrefs.GetFloat("score");
    }
}
