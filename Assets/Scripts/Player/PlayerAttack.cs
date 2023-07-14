using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;  //время перезарядки
    [SerializeField] private Transform firePoint;  //позиция, из которой будут выпущены снаряды
    [SerializeField] private GameObject[] fireballs;  //массив снарядов
    [SerializeField] private AudioClip fireballSound;

    [Header("Components")]
    [SerializeField] private Animator anim;
    [SerializeField] private Player playerMovement;
    private float cooldownTimer = Mathf.Infinity;   //таймер перезарядки

    //private bool isClickedAttack; //проверка нажатия кнопки

    //public void TaskOnClick()
    //{
        //isClickedAttack = true;
    //}

    private void Update()
    {
        //проверка нажата ли ЛКМ, время перезарядки и возможность атаки
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())  
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundControl.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        //при каждой атаке у одного шара из массива меняется положение на огневой точки
        fireballs[FindFireball()].transform.position = firePoint.position;   
        //извлечь компонент снаряда и послать в нужном направлении
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));

        //isClickedAttack = false;
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
}
