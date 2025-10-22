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
    public SliderLogic sldTiempo;
    public float ratioPerdidaEnergia;
    public SliderLogic sldEnergia;
    public GameObject jugador;
    public GameObject meta;
    public Slider avanceJugador;

    private Vector3 posInicial;
    private Vector3 posMeta;
    private float distInicial;
    private float tiempoRestante;

    private float energia;

    private int estrellas;
    
    void Start()
    {
        posInicial = jugador.transform.position;
        posMeta = meta.transform.position;
        distInicial = Vector2.Distance(posInicial, posMeta);
        tiempoRestante = tiempo;
        energia = 100.0f;
        estrellas = 0;

        sldEnergia.SetMaxValue(100.0f);
        sldTiempo.SetMaxValue(tiempo);
    }
    
    void Update()
    {
        sldTiempo.SetValue(tiempoRestante);
        sldEnergia.SetValue(energia);

        CountdownLogic();
        EnergyLossLogic();

        avanceJugador.value = Mathf.Clamp(1.0f - Vector2.Distance(jugador.transform.position, posMeta) / distInicial, 0.0f, 1.0f);
    }

    public void AddEnergy(float e)
    {
        energia = Mathf.Min(energia + e, 100.0f);
        sldEnergia.SetValue(energia);
    }

    public void AddTime(float t)
    {
        tiempoRestante = Mathf.Min(tiempoRestante + t, tiempo);
        sldTiempo.SetValue(tiempoRestante);
    }

    public void Win()
    {
        Time.timeScale = 0.0f;
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
