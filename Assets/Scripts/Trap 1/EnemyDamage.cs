using UnityEngine;

public class EnemyDamage : MonoBehaviour    //скрипт для нанесения урона
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hero")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}