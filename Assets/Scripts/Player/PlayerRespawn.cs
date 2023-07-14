using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;        //последний чекпоинт

    [Header("Components")]
    [SerializeField] private Health playerHealth;
    private UIControl uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIControl>();
    }

    public void CheckRespawn()
    {
        //проверка доступен ли чекпоинт
        if (currentCheckpoint == null)
        {
            //показать окно окончани€ игры
            PlayerData.Coins = 0;
            uiManager.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position;        //переместить игрока на чекпоинт
        playerHealth.Respawn();                                 //восстановить здоровье, анимацию и неу€звимость
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;                     //сохранить текущую точку как контрольную
            SoundControl.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;          //выключить коллайдер чекпоинта
            collision.GetComponent<Animator>().SetTrigger("appear");     //активировать анимацию
        }
    }
}
