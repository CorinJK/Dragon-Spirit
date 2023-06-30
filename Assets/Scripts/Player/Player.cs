using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;           //скорость
    [SerializeField] private float jumpPower;       //сила прыжка

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;      //сколько времени после схождения с поверхности игрок может совершить прыжок
    private float coyoteСounter;                    //таймер, сколько времени игрок вне поверхности

    [Header("Multiple Jump")]
    [SerializeField] private int extraJumps;        //количество прыжков
    private int jumpCounter;                        //счётчик прыжков

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpX;        //смещение по горизонтали
    [SerializeField] private float wallJumpY;        //смещение вверх

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Sound")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Particles")]
    [SerializeField] private SpawnParticle footStep;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    //public Joystick joystick;       //джостик
    //private bool isClickedJump;   //проверка нажатия кнопки

    //public void TaskOnClick()
    //{
        //isClickedJump = true;
    //}

    private void Awake()
    {
        //дают ссылки на компоненты из игрового объекта
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //меняет спрайт при движении налево
        if (horizontalInput > 0f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < 0f)
            transform.localScale = new Vector3(-1, 1, 1);

        //аниматор
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //прыжок
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //регулировка высоты прыжка
        if (Input.GetKeyDown(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteСounter = coyoteTime;         //возобносление способности на поверхности
                jumpCounter = extraJumps;           //сбрасывает сётчик доп. прыжков
            }
            else
                coyoteСounter -= Time.deltaTime;    //отсчёт вне поверхности 
        }
    }

    private void Jump()
    {
        if (coyoteСounter <= 0 && !onWall() && jumpCounter <= 0) return;     //отменяет метод прыжка
        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                //игрок не на земле, а coyoteСounter больше 0, сделать обычный прыжок
                if (coyoteСounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    //если есть доп. прыжки, то прыгнуть и уменьшить на 1
                    if(jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }
            //чтобы избежать двойных прыжков
            coyoteСounter = 0;
        }
        //isClickedJump = false;
    }

    //прыжок по стене
    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    //проверка на опору
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //проверка на стену
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //может ли игрок атаковать
    public bool canAttack()
    {
        return !onWall();
    }

    public void OnInteract()
    {

    }

    //партиклы бега
    public void SpawnFootDust()
    {
        footStep.Spawn();
    }
}