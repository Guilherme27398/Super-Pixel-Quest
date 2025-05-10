using UnityEngine;


public class Mushroom : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] bool moveLeft = false;

    void Update()
    {
        if (moveLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3) 
        {
            moveLeft = !moveLeft;
        }

        if (collision.CompareTag("Player"))
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.isGrow = true;
                Destroy(gameObject);
            }
        }
    }
}
