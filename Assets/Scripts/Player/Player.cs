using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;           //��������
    [SerializeField] private float jumpPower;       //���� ������

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;      //������� ������� ����� ��������� � ����������� ����� ����� ��������� ������
    private float coyote�ounter;                    //������, ������� ������� ����� ��� �����������

    [Header("Multiple Jump")]
    [SerializeField] private int extraJumps;        //���������� �������
    private int jumpCounter;                        //������� �������

    [Header("Wall Jump")]
    [SerializeField] private float wallJumpX;        //�������� �� �����������
    [SerializeField] private float wallJumpY;        //�������� �����

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

    //public Joystick joystick;       //�������
    //private bool isClickedJump;   //�������� ������� ������

    //public void TaskOnClick()
    //{
        //isClickedJump = true;
    //}

    private void Awake()
    {
        //���� ������ �� ���������� �� �������� �������
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //������ ������ ��� �������� ������
        if (horizontalInput > 0f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < 0f)
            transform.localScale = new Vector3(-1, 1, 1);

        //��������
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //������
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //����������� ������ ������
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
                coyote�ounter = coyoteTime;         //������������� ����������� �� �����������
                jumpCounter = extraJumps;           //���������� ����� ���. �������
            }
            else
                coyote�ounter -= Time.deltaTime;    //������ ��� ����������� 
        }
    }

    private void Jump()
    {
        if (coyote�ounter <= 0 && !onWall() && jumpCounter <= 0) return;     //�������� ����� ������
        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
            WallJump();
        else
        {
            if (isGrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                //����� �� �� �����, � coyote�ounter ������ 0, ������� ������� ������
                if (coyote�ounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    //���� ���� ���. ������, �� �������� � ��������� �� 1
                    if(jumpCounter > 0)
                    {
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }
            //����� �������� ������� �������
            coyote�ounter = 0;
        }
        //isClickedJump = false;
    }

    //������ �� �����
    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        wallJumpCooldown = 0;
    }

    //�������� �� �����
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //�������� �� �����
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    //����� �� ����� ���������
    public bool canAttack()
    {
        return !onWall();
    }

    public void OnInteract()
    {

    }

    //�������� ����
    public void SpawnFootDust()
    {
        footStep.Spawn();
    }
}