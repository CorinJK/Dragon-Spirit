using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;      //��������� ��������
    public float currentHealth { get; private set; }    //������� ��������, � ������ Healthbar
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;     //����������������� ������������
    [SerializeField] private float numberOfFlashes;     //���������� �������

    [Header("Invulnerable")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;                          //������������

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    //��������� �����
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;       //���� ��������, �� �������
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);    //����� �� ���� ������ 0 � �� ������ ���������

        //�������� ����� �� �����
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());      //������ ��������
            SoundControl.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                //�������������� ��� ������������ ������
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);         //����� ��� ������ �� �������� � ������ ��������
                anim.SetTrigger("die");

                dead = true;
                SoundControl.instance.PlaySound(deathSound);
            }
        }
    }

    //���������� ��������
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    //������������
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(3, 6, true);         //������������ ������������ �����������

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);                                //0.5f ��� ������������
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));   //�����
            spriteRend.color = Color.white;                                             //��� �������� ���, �� � ������� ������� ����� ���� ������� ���������
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(3, 6, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Respawn()
    {
        dead = false;
        
        AddHealth(startingHealth);  //��������������� ��������
        anim.ResetTrigger("die");   //�������� �������� ������
        anim.Play("idle");          //����������� �� �������� ��������
        StartCoroutine(Invulnerability());    //�������� ������������

        //������������ ��� ������������ ������
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}
