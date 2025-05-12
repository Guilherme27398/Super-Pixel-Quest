using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicaFundo;
    public AudioSource musicaVitoria;
    public AudioSource musicaMorte;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        TocarMusicaFundo();
    }

    public void TocarMusicaFundo()
    {
        PararTodas();
        musicaFundo.Play();
    }

    public void TocarMusicaVitoria()
    {
        PararTodas();
        musicaVitoria.Play();
    }

    public void TocarMusicaMorte()
    {
        PararTodas();
        musicaMorte.Play();
    }

    private void PararTodas()
    {
        musicaFundo.Stop();
        musicaVitoria.Stop();
        musicaMorte.Stop();
    }
}


