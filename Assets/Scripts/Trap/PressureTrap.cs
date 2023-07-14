using System.Collections;
using UnityEngine;

public class PressureTrap : MonoBehaviour
{
    [SerializeField] private float damage;              //���� �� �������

    [Header("Pressure Trap")]
    [SerializeField] private float activationDelay;     //����� ����� ������� �� ���������
    [SerializeField] private float activeTime;          //��� ����� ����� �������

    [Header("SFX")]
    [SerializeField] private AudioClip pressureTrapSound;

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRend;

    private bool triggered;     //������� �����������
    private bool active;        //������� �������������� � ������� ����

    private Health playerHealth;

    //����� ���� �������� ����� ����� �� ��������� �� �������
    private void Update()
    {
        if(playerHealth != null && active)
            playerHealth.TakeDamage(damage);
    }

    //�������� ������������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            playerHealth = collision.GetComponent<Health>();

            //���������� �����������
            if (!triggered)
                StartCoroutine(ActivatePressureTrap());

            //������� ������������
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
            playerHealth = null;
    }

    //������� ��������
    private IEnumerator ActivatePressureTrap()
    {
        //������ � �������, ��������� ������
        triggered = true;
        spriteRend.color = Color.red;

        //����� ��������, ���������, ��������, ������� � ������� ����
        yield return new WaitForSeconds(activationDelay);
        SoundControl.instance.PlaySound(pressureTrapSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //���������, ����������� �������, ����� ����������
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
