using UnityEngine;

public class EnemySideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;    //дальность перемещения
    [SerializeField] private float speed;               //скорость
    [SerializeField] private float damage;              //урон
    private bool movingLeft;                            //перемещение влево
    private float leftEdge;                             //левый край
    private float rightEdge;                            //правый край

    //определение дальности перемещения
    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        //проверка направления движения
        if (movingLeft)
        {
            //проверка не дальше ли края
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);    //само перемещение
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Hero")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}