using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameUI interfazJuego;
    public GameObject interfazPausa;
    public GameObject interfazVictoria;
    public GameObject interfazDerrota;
    public float tiempo;
    private bool pausa = false;
    private int siguienteNivel;

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

    public bool EstadoPausa
    {
        get
        {
            return pausa;
        }
        set
        {
            pausa = value;
            if (pausa)
                Pausa();
            else
                Resumir();
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

        siguienteNivel = SceneManager.GetActiveScene().buildIndex + 1;
    }

    void Update()
    {
        CuentaRegresiva();
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

    public void Pausa()
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
        Time.timeScale = 0.0f;
        interfazJuego.gameObject.SetActive(false);
        interfazVictoria.SetActive(true);
    }

    public void Derrota()
    {
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

    public void SiguienteNivel(int n)
    {
        if (siguienteNivel == 3)
            RegresarSelector();

        SceneManager.LoadScene(siguienteNivel);
    }
}
