using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float score { get; private set; }
    [SerializeField] Text scoreText;
    [SerializeField] private AudioClip scoreSound;

    private void Awake()
    {
        score = PlayerData.Coins;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Score")
        {
            Destroy(collision.gameObject);
            SoundControl.instance.PlaySound(scoreSound);

            score++;
            PlayerData.Coins = score;
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }
}