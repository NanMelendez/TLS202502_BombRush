using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    public InputActionReference pauseControl;
    public GameObject btnSgte;
    public GameObject btnCerrar;
    private Vector3 posInicial;
    private Vector3 posMeta;
    private float distInicial;
    private float tiempoRestante;
    private float energia = 100.0f;
    private int estrellas = 0;
    private bool pauseToggle = false;
    
    void Start()
    {
        posInicial = jugador.transform.position;
        posMeta = meta.transform.position;
        distInicial = Vector2.Distance(posInicial, posMeta);
        tiempoRestante = tiempo;

        sldEnergia.SetMaxValue(100.0f);
        sldTiempo.SetMaxValue(tiempo);

        btnSgte.SetActive(false);
        btnCerrar.SetActive(false);
    }

    void Update()
    {
        float pauseBtn = pauseControl.action.ReadValue<float>();
        pauseToggle ^= pauseBtn != 0.0f;

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
        btnSgte.SetActive(true);
        btnCerrar.SetActive(true);
    }

    public float GetRemainingTime()
    {
        return tiempoRestante;
    }

    public void RedoLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel(string name)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(name);
    }

    public void CloseGame() {
        Application.Quit();
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
            energia -= ratioPerdidaEnergia * Time.deltaTime;

        if (energia < 0.0f)
            energia = 0.0f;
    }
}
