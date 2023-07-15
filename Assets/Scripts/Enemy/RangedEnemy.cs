using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;      //время перезарядки
    [SerializeField] private float range;               //диапазон
    [SerializeField] private float damage;              //урон врага

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;       //огневая позиция
    [SerializeField] private GameObject[] fireballs;     //огненные шары

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;     //расстояние до коллайдера атаки
    [SerializeField] private BoxCollider2D BoxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;       //таймер перезарядки

    [Header("Fireball Sound")]
    [SerializeField] private AudioClip firballSound;

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyPatrol enemyPatrol;

    //проверка готовности врага к атаке
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }

        //активация патруля в зависимости есль ли игроок в поле зрения
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private void RangedAttack()
    {
        SoundController.instance.PlaySound(firballSound);
        cooldownTimer = 0;
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    //проверка снарядов
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)    //проверка активен ли шар
                return i;                           //тода вернем индекс
        }
        return 0;
    }

    //проверка на игрока
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    //рисует линию атаки
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z));
    }
}
