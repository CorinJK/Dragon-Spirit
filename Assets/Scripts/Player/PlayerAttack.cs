using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;  //����� �����������
    [SerializeField] private Transform firePoint;  //�������, �� ������� ����� �������� �������
    [SerializeField] private GameObject[] fireballs;  //������ ��������
    [SerializeField] private AudioClip fireballSound;

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private Player playerMovement;
    private float cooldownTimer = Mathf.Infinity;   //������ �����������

    //private bool isClickedAttack; //�������� ������� ������

    //public void TaskOnClick()
    //{
        //isClickedAttack = true;
    //}

    private void Update()
    {
        //�������� ������ �� ���, ����� ����������� � ����������� �����
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())  
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundControl.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //��� ������ ����� � ������ ���� �� ������� �������� ��������� �� ������� �����
        fireballs[FindFireball()].transform.position = firePoint.position;   
        //������� ��������� ������� � ������� � ������ �����������
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        //isClickedAttack = false;
    }

    //�������� ��������
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)    //�������� ������� �� ���
                return i;                           //���� ������ ������
        }
        return 0;
    }
}
