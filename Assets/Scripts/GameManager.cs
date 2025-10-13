using System;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Tooltip("Cuenta regresiva en segundos antes de que el jugador explote")]
    public float tiempo;
    public TextMeshProUGUI txtTiempo;
    public Slider sldTiempo;
    public TextMeshProUGUI txtEnergia;
    public Slider sldEnergia;
    public GameObject jugador;
    public GameObject meta;
    public Slider avanceJugador;

    private Vector3 posInicial;
    private Vector3 posMeta;
    private float distInicial;
    private float tiempoRestante;

    private float energia;
    [SerializeField]
    private float ratioPerdidaEnergia;

    private int estrellas;

    private bool juegoTerminado;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posInicial = jugador.transform.position;
        posMeta = meta.transform.position;
        distInicial = Vector2.Distance(posInicial, posMeta);
        tiempoRestante = tiempo;
        energia = 100.0f;
        estrellas = 0;
        juegoTerminado = false;
    }

    // Update is called once per frame
    void Update()
    {
        sldTiempo.value = tiempoRestante / tiempo;
        txtTiempo.text = TimeToStr(tiempoRestante);

        sldEnergia.value = energia / 100.0f;
        txtEnergia.text = EnergyToStr(energia);

        if (!juegoTerminado)
        {
            CountdownLogic();
            EnergyLossLogic();
        }

        avanceJugador.value = Mathf.Clamp(1.0f - Vector2.Distance(jugador.transform.position, posMeta) / distInicial, 0.0f, 1.0f);
    }

    private string TimeToStr(float t)
    {
        int minutes = Mathf.FloorToInt(t / 60.0f);
        int seconds = Mathf.FloorToInt(t % 60.0f);

        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private string EnergyToStr(float e)
    {
        return string.Format("{0}%", Mathf.Round(e));
    }

    public void AddTime(float t)
    {
        tiempoRestante = Mathf.Min(tiempoRestante + t, tiempo);
    }

    public void AddEnergy(float e)
    {
        energia = Mathf.Min(energia + e, 100.0f);
    }

    public bool getJuegoTerminado()
    {
        return juegoTerminado;
    }

    public void Win()
    {
        juegoTerminado = true;
        CalculateStars();
        Debug.Log("Estrellas obtenidas: " + estrellas);
    }

    private void CalculateStars()
    {
        if (energia > 75.0f)
            estrellas = 3;
        else if (energia > 50.0f)
            estrellas = 2;
        else if (energia > 25.0f)
            estrellas = 1;
        else
            estrellas = 0;
    }

    private void CountdownLogic()
    {
        if (tiempoRestante > 0.0f)
            tiempoRestante -= Time.deltaTime;

        if (tiempoRestante < 0.0f)
            tiempoRestante = 0.0f;
    }

    private void EnergyLossLogic()
    {
        if (energia > 0.0f)
            energia -= ratioPerdidaEnergia;

        if (energia < 0.0f)
            energia = 0.0f;
    }
}
