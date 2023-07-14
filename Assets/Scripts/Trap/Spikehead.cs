using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spike Attacking")]
    [SerializeField] private float speed;       //скорость движения
    [SerializeField] private float range;       //диапазон видимости
    [SerializeField] private float checkDelay;  //задержка
    [SerializeField] private LayerMask HeroLayer;

    private Vector3[] directions = new Vector3[4];  //хранит направления
    private Vector3 destination;    //место назначения
    private float checkTime;        //таймер
    private bool attacking;         //атака

    [Header("SFX")]
    [SerializeField] private AudioClip impactSound;

    //вызывается каждый раз при активации объекта
    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);      //само перемещение
        else
        {
            //по времени проверка на игрока
            checkTime += Time.deltaTime;
            if (checkTime > checkDelay)
                CheckForPlayer();
        }
    }

    //проверка игрока
    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);    //рисует векторы, по которым проверка
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, HeroLayer);      //обнаружение попадания

            //игрок найден и ловушка еще не атакует
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTime = 0;
            }
        }
    }

    //просчитывает направление
    private void CalculateDirections()
    {
        directions[0] = transform.right * range;    //вправо
        directions[1] = -transform.right * range;   //влево
        directions[2] = transform.up * range;       //вверх
        directions[3] = -transform.up * range;      //вниз
    }

    //останавливает при положении = конечной точке
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundControl.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}
