using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;      //время перезарядки
    [SerializeField] private Transform firePoint;       //огневая точка
    [SerializeField] private GameObject[] arrow;
    private float cooldownTimer;                        //таймер перезарядки

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Attack()
    {
        cooldownTimer = 0;

        SoundManager.instance.PlaySound(arrowSound);
        arrow[FindArrow()].transform.position = firePoint.position;
        arrow[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    //проверка снарядов
    private int FindArrow()
    {
        for (int i = 0; i < arrow.Length; i++)
        {
            if (!arrow[i].activeInHierarchy)    //проверка активена ли стрела
                return i;                       //тогда вернем индекс
        }
        return 0;
    }

    //расчёт перезарядки
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer > attackCooldown)
            Attack();
    }
}
