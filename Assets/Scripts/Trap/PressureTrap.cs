using System.Collections;
using UnityEngine;

public class PressureTrap : MonoBehaviour
{
    [SerializeField] private float damage;              //урон от ловушки

    [Header("Pressure Trap")]
    [SerializeField] private float activationDelay;     //время после нажатия до активации
    [SerializeField] private float activeTime;          //как долго будет активна

    [Header("SFX")]
    [SerializeField] private AudioClip pressureTrapSound;

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer spriteRend;

    private bool triggered;     //ловушка запустилась
    private bool active;        //ловушка активировалась и наносит урон

    private Health playerHealth;

    //чтобы урон проходил когда игрок не двигается на объекте
    private void Update()
    {
        if(playerHealth != null && active)
            playerHealth.TakeDamage(damage);
    }

    //проверка столкновения
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
        {
            playerHealth = collision.GetComponent<Health>();

            //коллайдеры столкнулись
            if (!triggered)
                StartCoroutine(ActivatePressureTrap());

            //ловушка активирована
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
            playerHealth = null;
    }

    //рассчёт задержки
    private IEnumerator ActivatePressureTrap()
    {
        //красит в красный, уведомляя игрока
        triggered = true;
        spriteRend.color = Color.red;

        //ждать задержку, активация, анимация, вернуть в обычный цвет
        yield return new WaitForSeconds(activationDelay);
        SoundControl.instance.PlaySound(pressureTrapSound);
        spriteRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        //подождать, деактивация ловушки, сброс переменных
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
