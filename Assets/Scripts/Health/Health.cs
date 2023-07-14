using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;      //начальное здоровье
    public float currentHealth { get; private set; }    //текущее здоровье, в скрипт Healthbar
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;     //продолжительность неуязвимости
    [SerializeField] private float numberOfFlashes;     //количество миганий

    [Header("Invulnerable")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;                          //неуязвимость

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

    //нанесение урона
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;       //если неуязвим, то вернуть
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);    //чтобы не было меньше 0 и не больше максимума

        //проверка мертв ли игрок
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());      //запуск коротины
            SoundControl.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                //деактивировать все подключенные классы
                foreach (Behaviour component in components)
                    component.enabled = false;

                anim.SetBool("grounded", true);         //чтобы при смерти не зависало в другой анимации
                anim.SetTrigger("die");

                dead = true;
                SoundControl.instance.PlaySound(deathSound);
            }
        }
    }

    //увеличение здоровья
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    //неуязвимость
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(3, 6, true);         //игнорировать столкновение коллайдеров

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);                                //0.5f это прозрачность
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));   //ждать
            spriteRend.color = Color.white;                                             //тут написали так, тк в прошлом примере нужно было вручную настроить
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
        
        AddHealth(startingHealth);  //восстанавливаем здоровье
        anim.ResetTrigger("die");   //отменяем анимация смерти
        anim.Play("idle");          //переключаем на анимацию ожидания
        StartCoroutine(Invulnerability());    //включаем неуязвимость

        //активировать все подключенные классы
        foreach (Behaviour component in components)
            component.enabled = true;
    }
}
