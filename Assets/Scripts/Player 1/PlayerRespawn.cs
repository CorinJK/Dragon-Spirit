using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;        //последний чекпоинт

    [Header("Components")]
    [SerializeField] private Health playerHealth;
    private UIController uiController;

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();
    }

    public void CheckRespawn()
    {
        //проверка доступен ли чекпоинт
        if (currentCheckpoint == null)
        {
            //показать окно окончани€ игры
            PlayerData.Coins = 0;
            uiController.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position;        //переместить игрока на чекпоинт
        playerHealth.Respawn();                                 //восстановить здоровье, анимацию и неу€звимость

        //переместить камеру в чекпоинт (должен быть дочерним элементом)
        //Camera.main.GetComponent<CameraControl>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;                     //сохранить текущую точку как контрольную
            SoundController.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;          //выключить коллайдер чекпоинта
            collision.GetComponent<Animator>().SetTrigger("appear");     //активировать анимацию
        }
    }
}
