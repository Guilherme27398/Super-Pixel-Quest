using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI moedasTexto;
    private int moedas = 0;

    void Start()
    {
        AtualizarHUD();
    }

    public void AdicionarMoeda()
    {
        moedas++;
        AtualizarHUD();
    }

    private void AtualizarHUD()
    {
        moedasTexto.text = "Moedas: " + moedas.ToString();
    }
}
