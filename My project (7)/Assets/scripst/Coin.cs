using UnityEngine;

public class Moeda : MonoBehaviour
{
    private HUDController hud;
    private bool foiApanhada = false; 

    private void Start()
    {
        hud = FindObjectOfType<HUDController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (foiApanhada) return; 

        if (collision.CompareTag("Player"))
        {
            foiApanhada = true; 
            if (hud != null)
            {
                hud.AdicionarMoeda();
            }
            else
            {
                Debug.LogError("HUDController não encontrado!");
            }

            Destroy(gameObject);
        }
    }
}

