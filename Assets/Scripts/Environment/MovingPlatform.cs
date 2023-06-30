using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Edge Points")]
    [SerializeField] private Transform leftEdge;    //����� ����
    [SerializeField] private Transform rightEdge;   //������ ����

    [Header("Platform")]
    [SerializeField] private Transform platform;

    [Header("Movement parameters")]
    [SerializeField] private float speed;       //��������
    private bool movingLeft;                    //�������� �����

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;     //����� ������� ����� � ����
    private float idleTimer;                         //������ ������� ����� � ����

    private void Update()
    {
        if (movingLeft)
        {
            if (platform.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (platform.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    //����������� ��������
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        //�������� �� �����������
        platform.position = new Vector3(platform.position.x + Time.deltaTime * _direction * speed, platform.position.y, platform.position.z);
    }
}
