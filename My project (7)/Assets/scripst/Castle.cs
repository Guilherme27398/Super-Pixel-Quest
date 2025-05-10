using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.TocarMusicaVitoria();

            Invoke("CarregarCenaVitoria", 3f);
        }
    }

    private void CarregarCenaVitoria()
    {
        SceneManager.LoadScene("WinScene");
    }
}
