using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] bool isJump;
    [SerializeField] bool inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    Animator animPlayer;
    CapsuleCollider2D playerCollider;

    [HideInInspector] public bool isGrow = false;
    bool isBig = false;
    [SerializeField] public bool dead = false;

    [SerializeField] bool isInvincible = false;
    [SerializeField] float invincibleTime = 2.0f;
    [SerializeField] float invincibleTimer = 0f;

    private void Awake()
    {
        animPlayer = GetComponent<Animator>();
        rbPlayer = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        dead = false;
    }

    private void Update()
    {
        if (dead) return;

        if (transform.position.y < -10f)
        {
            dead = true;
            RestartGame();
        }

        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.blue);
        animPlayer.SetBool("jump", !inFloor);

        if (Input.GetButtonDown("Jump") && inFloor)
            isJump = true;
        else if (Input.GetButtonUp("Jump") && rbPlayer.linearVelocity.y > 0)
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, rbPlayer.linearVelocity.y * 0.5f);

        CheckGrow();

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            float remainder = invincibleTimer % 0.2f;
            GetComponent<SpriteRenderer>().enabled = remainder > 0.1f;

            if (invincibleTimer <= 0)
            {
                isInvincible = false;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void FixedUpdate()
    {
        Move();
        JumpPlayer();
    }

    void Move()
    {
        if (dead) return;

        float xmove = Input.GetAxis("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xmove * speed, rbPlayer.linearVelocity.y);
        animPlayer.SetFloat("speed", Mathf.Abs(xmove));

        if (xmove > 0)
            transform.eulerAngles = new Vector2(0, 0);
        else if (xmove < 0)
            transform.eulerAngles = new Vector2(0, 180);
    }

    void JumpPlayer()
    {
        if (dead) return;

        if (isJump)
        {
            rbPlayer.linearVelocity = Vector2.up * jumpForce;
            isJump = false;
        }
    }

    void CheckGrow()
    {
        if (isGrow && !isBig)
        {
            Grow();
            isGrow = false;
        }
    }

    void Grow()
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1);
        isBig = true;

        if (playerCollider != null)
        {
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y * 1.5f);
            playerCollider.offset = new Vector2(playerCollider.offset.x, playerCollider.offset.y + 0.25f);
        }
    }

    public void Shrink()
    {
        isBig = false;
        transform.localScale = new Vector3(1f, 1f, 1);

        if (playerCollider != null)
        {
            playerCollider.size = new Vector2(playerCollider.size.x, playerCollider.size.y / 1.5f);
            playerCollider.offset = new Vector2(playerCollider.offset.x, playerCollider.offset.y - 0.25f);
        }

        isInvincible = true;
        invincibleTimer = invincibleTime;

        Vector2 knockback = new Vector2(transform.eulerAngles.y == 0 ? -1 : 1, 1) * 5f;
        rbPlayer.linearVelocity = knockback;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public bool IsBig()
    {
        return isBig;
    }

    public void Death()
    {
        if (!dead)
        {
            AudioManager.instance.TocarMusicaMorte(); 
            StartCoroutine(DeathCorotine());
        }
    }

    IEnumerator DeathCorotine()
    {
        dead = true;
        animPlayer.SetTrigger("death");
        yield return new WaitForSeconds(0.5f);

        rbPlayer.linearVelocity = Vector2.zero;
        rbPlayer.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        playerCollider.isTrigger = true;

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        if (enemyLayer != -1)
            Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayer, true);

        Invoke("RestartGame", 2.5f);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("FASE_1");
        AudioManager.instance.TocarMusicaFundo(); 
    }
}
