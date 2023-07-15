using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;        //��������� ��������

    [Header("Components")]
    [SerializeField] private Health playerHealth;
    private UIController uiController;

    private void Awake()
    {
        uiController = FindObjectOfType<UIController>();
    }

    public void CheckRespawn()
    {
        //�������� �������� �� ��������
        if (currentCheckpoint == null)
        {
            //�������� ���� ��������� ����
            PlayerData.Coins = 0;
            uiController.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position;        //����������� ������ �� ��������
        playerHealth.Respawn();                                 //������������ ��������, �������� � ������������

        //����������� ������ � �������� (������ ���� �������� ���������)
        //Camera.main.GetComponent<CameraControl>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;                     //��������� ������� ����� ��� �����������
            SoundController.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;          //��������� ��������� ���������
            collision.GetComponent<Animator>().SetTrigger("appear");     //������������ ��������
        }
    }
}
