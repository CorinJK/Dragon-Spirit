using UnityEngine;

public class EnemyProjectile : EnemyDamage  //������� ���� ��� ������ �������
{
    [SerializeField] private float speed;       //�������� �������
    [SerializeField] private float resetTime;   //����� ����������� �������
    private float livetime;                     //����� ������������� �������

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider2D boxCollider;

    private bool hit;

    //��������� �������
    public void ActivateProjectile()
    {
        hit = false;
        livetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;                                //���� �����, �� ��������������

        float movementSpeed = speed * Time.deltaTime;   //�������� ��������
        transform.Translate(movementSpeed, 0, 0);       //����������� �������

        //�������� ������� �������������
        livetime += Time.deltaTime;
        if (livetime > resetTime)
            gameObject.SetActive(false);
    }
     
    //������������ ���� ������� � ������ ���������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        base.OnTriggerEnter2D(collision);   //������� ���� �������� ���������� ��������
        boxCollider.enabled = false;

        if (anim != null)                   //���� ��� �������� ��� - ���������
            anim.SetTrigger("explode");
        else 
            gameObject.SetActive(false);    //������ ������������
    }

    //����������� �������
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
