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
    [SerializeField] private ParticleSystem jumpParticle;
    [SerializeField] private GameObject slamParticle;
    [SerializeField] private float slamDownVelocity;
    [SerializeField] private float damageVelocity;

    [Header("Components")]
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Animator anim;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Health playerHealth;

    private float horizontalInput;
    public bool canMove = true;

    //public Joystick joystick;       //�������
    //private bool isClickedJump;   //�������� ������� ������

    //public void TaskOnClick()
    //{
    //isClickedJump = true;
    //}

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //������ ������ ��� �������� ������

        if (horizontalInput > 0f && canMove)
            transform.localScale = Vector3.one;
        else if (horizontalInput < 0f && canMove)
            transform.localScale = new Vector3(-1, 1, 1);

        //��������
        anim.SetBool("run", horizontalInput != 0 && canMove);
        anim.SetBool("grounded", isGrounded());

        //������
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
            Jump();

        //����������� ������ ������
        if (Input.GetKeyDown(KeyCode.Space) && body.velocity.y > 0 && canMove)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (onWall() && canMove)
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else if (canMove)
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
        SoundController.instance.PlaySound(jumpSound);
        SpawnJumpParticle();

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
                    if (jumpCounter > 0)
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
        return !onWall() && canMove;
    }

    // �������� ��� ������
    private void SpawnJumpParticle()
    {
        jumpParticle.Play();
    }

    // ���� �� �������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.IsInLayer(groundLayer))
        {
            var contact = collision.contacts[0];
            if (contact.relativeVelocity.y >= slamDownVelocity)
            {
                slamParticle.SetActive(true);
            }

            if (contact.relativeVelocity.y >= damageVelocity)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }
}