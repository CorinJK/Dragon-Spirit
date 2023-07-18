using UnityEngine;

public class EnemyProjectile : EnemyDamage  //наносит урон при каждом касании
{
    [SerializeField] private float speed;       //скорость снаряда
    [SerializeField] private float resetTime;   //время деактивации объекта
    private float livetime;                     //время существования снаряда

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider2D boxCollider;

    private bool hit;

    //активация снаряда
    public void ActivateProjectile()
    {
        hit = false;
        livetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;                                //если попал, то деактивировать

        float movementSpeed = speed * Time.deltaTime;   //скорость движения
        transform.Translate(movementSpeed, 0, 0);       //перемещение снаряда

        //проверка времени существования
        livetime += Time.deltaTime;
        if (livetime > resetTime)
            gameObject.SetActive(false);
    }
     
    //деактивирует если попадет в другой коллайдер
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;

        base.OnTriggerEnter2D(collision);   //наносит урон согласно ссылаемому сценарию
        boxCollider.enabled = false;

        if (anim != null)                   //если это огненный шар - взоравать
            anim.SetTrigger("explode");
        else 
            gameObject.SetActive(false);    //стрелу деактивирует
    }

    //деактивация снаряда
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
