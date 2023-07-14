using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;        //��������� ��������

    [Header("Components")]
    [SerializeField] private Health playerHealth;
    private UIControl uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIControl>();
    }

    public void CheckRespawn()
    {
        //�������� �������� �� ��������
        if (currentCheckpoint == null)
        {
            //�������� ���� ��������� ����
            PlayerData.Coins = 0;
            uiManager.GameOver();

            return;
        }

        transform.position = currentCheckpoint.position;        //����������� ������ �� ��������
        playerHealth.Respawn();                                 //������������ ��������, �������� � ������������
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;                     //��������� ������� ����� ��� �����������
            SoundControl.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;          //��������� ��������� ���������
            collision.GetComponent<Animator>().SetTrigger("appear");     //������������ ��������
        }
    }
}
