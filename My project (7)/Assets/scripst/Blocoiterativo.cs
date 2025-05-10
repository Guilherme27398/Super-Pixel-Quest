using UnityEngine;

public class BlocoInterativo : MonoBehaviour
{
    public GameObject itemParaLiberar; // Prefab da moeda (ou cogumelo)
    public Sprite blocoUsadoSprite;
    private bool foiUsado = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (foiUsado) return;

        foreach (ContactPoint2D ponto in collision.contacts)
        {
            if (ponto.normal.y > 0.5f)
            {
                AtivarBloco();
                break;
            }
        }
    }

    void AtivarBloco()
    {
        if (itemParaLiberar != null)
        {
            Instantiate(itemParaLiberar, transform.position + Vector3.up * 1f, Quaternion.identity);
        }

        if (blocoUsadoSprite != null)
        {
            // Apenas troca o sprite, mantendo a física
            GetComponent<SpriteRenderer>().sprite = blocoUsadoSprite;
        }

        foiUsado = true;
    }
}
