using UnityEngine;

public class PiranhaPlant : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float waitTime = 2f;

    private Vector3 initialPosition;
    private bool isMovingUp = true;
    private float timer = 0f;

    private void Start()
    {
        initialPosition = transform.position;
        timer = waitTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (isMovingUp)
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition + Vector3.up * height, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, initialPosition + Vector3.up * height) < 0.01f)
                {
                    timer = waitTime;
                    isMovingUp = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
                {
                    timer = waitTime;
                    isMovingUp = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null && player.dead) return;

        if (player.IsBig() && !player.IsInvincible())
        {
            player.Shrink();
        }
        else if (!player.IsInvincible())
        {
            player.Death();
        }
    }
}
