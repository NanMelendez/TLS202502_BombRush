using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameUI interfazJuego;
    public GameObject interfazPausa;
    public GameObject interfazVictoria;
    public GameObject interfazDerrota;
    public GameObject jugador;
    public GameObject explosion;
    public CamShake camShake;
    public float tiempo;
    private bool gameover = false;
    private bool pausa = false;
    private bool victoria = false;
    private int siguienteNivel;
    private int totalNiveles;

    public float TiempoRestante
    {
        get
        {
            return interfazJuego.RemainingTime;
        }
        set
        {
            interfazJuego.RemainingTime = value;
        }
    }

    public float EnergiaRestante
    {
        get
        {
            return interfazJuego.RemainingEnergy;
        }
        set
        {
            interfazJuego.RemainingEnergy = value;
        }
    }

    public float TiempoLimite
    {
        get
        {
            return interfazJuego.MaxTime;
        }
        set
        {
            interfazJuego.MaxTime = value;
        }
    }

    public float EnergiaLimite
    {
        get
        {
            return interfazJuego.MaxEnergy;
        }
        set
        {
            interfazJuego.MaxEnergy = value;
        }
    }

    public bool Pausa
    {
        get
        {
            return pausa;
        }
        set
        {
            pausa = value;
            if (pausa)
                Pausar();
            else
                Resumir();
        }
    }

    public bool GameOver
    {
        get
        {
            return gameover;
        }
        set
        {
            gameover = value;
        }
    }

    public bool Ganaste
    {
        get
        {
            return victoria;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        
        TiempoLimite = tiempo;
        EnergiaLimite = 100.0f;

        interfazPausa.SetActive(false);
        interfazVictoria.SetActive(false);
        interfazDerrota.SetActive(false);

        siguienteNivel = SceneManager.GetActiveScene().buildIndex - 1;
        totalNiveles = SceneManager.sceneCountInBuildSettings - 3;
    }

    void Update()
    {
        CuentaRegresiva();

        if (TiempoRestante <= 0.0f && jugador != null && !gameover)
        {
            gameover = true;
            Instantiate(explosion, jugador.transform.position, Quaternion.identity);
            Destroy(jugador);
            camShake.ShakeCamera(2.5f, 7.5f);
            Invoke(nameof(Derrota), 1.75f);
        }

        if (gameover && jugador != null)
            MatarJugador();

        if (!gameover && TiempoRestante <= 0.0f)
            gameover = true;
    }

    private void MatarJugador()
    {
        gameover = true;
        Instantiate(explosion, jugador.transform.position, Quaternion.identity);
        Destroy(jugador);
        camShake.ShakeCamera(2.5f, 7.5f);
        Invoke(nameof(Derrota), 1.75f);
    }

    private void CuentaRegresiva()
    {
        if (TiempoRestante > 0.0f)
            TiempoRestante -= Time.deltaTime;

        if (TiempoRestante < 0.0f)
            TiempoRestante = 0.0f;
    }

    public void MasTiempo(float t)
    {
        TiempoRestante = Mathf.Min(TiempoRestante + t, TiempoLimite);
    }
    
    public void MasEnergia(float e)
    {
        EnergiaRestante = Mathf.Min(EnergiaRestante + e, EnergiaLimite);
    }

    public void Pausar()
    {
        interfazPausa.SetActive(true);
    }

    public void Resumir()
    {
        interfazPausa.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Victoria()
    {
        victoria = true;
        Time.timeScale = 0.0f;
        interfazJuego.gameObject.SetActive(false);
        interfazVictoria.SetActive(true);
    }

    private void Derrota()
    {
        gameover = true;
        Time.timeScale = 0.0f;
        interfazJuego.gameObject.SetActive(false);
        interfazDerrota.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RegresarSelector()
    {
        SceneManager.LoadScene("Selector Niveles");
    }

    public void SiguienteNivel()
    {
        SceneManager.LoadScene((siguienteNivel > totalNiveles) ? "Creditos" : "Nivel " + siguienteNivel);
    }
}
