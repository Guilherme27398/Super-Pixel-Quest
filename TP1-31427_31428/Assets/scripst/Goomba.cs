using UnityEngine;

public class Goomba : MonoBehaviour
{
    Rigidbody2D rbGoomba;
    [SerializeField] float speed = 2f;
    [SerializeField] Transform point1, point2;
    [SerializeField] LayerMask layer;
    [SerializeField] bool isColliding;

    Animator animGoomba;
    BoxCollider2D colliderGoomba;

    private void Awake()
    {
        rbGoomba = GetComponent<Rigidbody2D>();
        animGoomba = GetComponent<Animator>();
        colliderGoomba = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        rbGoomba.linearVelocity = new Vector2(speed, rbGoomba.linearVelocity.y);
        isColliding = Physics2D.Linecast(point1.position, point2.position, layer);

        if (isColliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null && player.dead) return;

        if (transform.position.y + 0.5f < collision.transform.position.y)
        {
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.AddForce(Vector2.up * 6, ForceMode2D.Impulse);
            }

            animGoomba.SetTrigger("death");
            speed = 0;
            colliderGoomba.enabled = false;
            Destroy(gameObject, 0.3f);
        }
        else
        {
            if (player.IsBig() && !player.IsInvincible())
            {
                player.Shrink();
            }
            else if (!player.IsInvincible())
            {
                player.Death();

                Goomba[] goombas = FindObjectsOfType<Goomba>();
                foreach (var g in goombas)
                {
                    g.speed = 0;
                    g.animGoomba.speed = 0;
                }
            }
        }
    }
}
