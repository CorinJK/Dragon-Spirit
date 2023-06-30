using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;   //скорость снаряда
    private float direction;                //будет определять направление полета
    private bool hit;
    private float lifetime;                 //время жизни снаряда

    private BoxCollider2D boxCollider;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;    //попадает ли шар во что-нибудь
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);    //перемещает шар со скоростью

        lifetime += Time.deltaTime;
        if (lifetime > 5) gameObject.SetActive(false);  //если дольше 5, то деактивация
    }

    //попал ли шар в какой-либо объект
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    //используем для самого выстрела, чтобы определить куда лететь
    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);     //проверка на активность
        hit = false;
        boxCollider.enabled = true;

        //поворот направления полета шара
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)      //проверка на верную сторону полета
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //деактивация шара
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
