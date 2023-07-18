using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;      //����� �����������
    [SerializeField] private float range;               //��������
    [SerializeField] private float damage;              //���� �����

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;     //���������� �� ���������� �����
    [SerializeField] private BoxCollider2D BoxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;       //������ �����������

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attackSound;

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyPatrol enemyPatrol;
    private Health playerHealth;

    //�������� ���������� ����� � �����
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                SoundController.instance.PlaySound(attackSound);
            }
        }

        //��������� ������� � ����������� ���� �� ������ � ���� ������
        if(enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    //�������� �� ������
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    //������ ����� �����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BoxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(BoxCollider.bounds.size.x * range, BoxCollider.bounds.size.y, BoxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //���� ����� ��� ��� � ���� ����� �����
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
    }
}
