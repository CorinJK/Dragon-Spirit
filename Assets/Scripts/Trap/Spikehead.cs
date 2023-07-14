using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spike Attacking")]
    [SerializeField] private float speed;       //�������� ��������
    [SerializeField] private float range;       //�������� ���������
    [SerializeField] private float checkDelay;  //��������
    [SerializeField] private LayerMask HeroLayer;

    private Vector3[] directions = new Vector3[4];  //������ �����������
    private Vector3 destination;    //����� ����������
    private float checkTime;        //������
    private bool attacking;         //�����

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    //���������� ������ ��� ��� ��������� �������
    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);      //���� �����������
        else
        {
            //�� ������� �������� �� ������
            checkTime += Time.deltaTime;
            if (checkTime > checkDelay)
                CheckForPlayer();
        }
    }

    //�������� ������
    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);    //������ �������, �� ������� ��������
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, HeroLayer);      //����������� ���������

            //����� ������ � ������� ��� �� �������
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTime = 0;
            }
        }
    }

    //������������ �����������
    private void CalculateDirections()
    {
        directions[0] = transform.right * range;    //������
        directions[1] = -transform.right * range;   //�����
        directions[2] = transform.up * range;       //�����
        directions[3] = -transform.up * range;      //����
    }

    //������������� ��� ��������� = �������� �����
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundControl.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
