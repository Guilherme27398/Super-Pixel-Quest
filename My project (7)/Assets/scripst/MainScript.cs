using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("FASE_1"); 
    }

    public void QuitGame()
    {
        Debug.Log("SAIR DO JOGO");
        Application.Quit();
    }
}
