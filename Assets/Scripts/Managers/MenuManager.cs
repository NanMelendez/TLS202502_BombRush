using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject preguntaSalir;
    [SerializeField]
    private GameObject btnCreditos;
    [SerializeField]
    private GameObject coverEquipo;

    void Start()
    {
        Debug.Log("¿Créditos desbloqueados?" + (PlayerPrefs.GetInt("CreditosDesbloqueados") == 1));
        Debug.Log("¿Juego iniciado?" + (PlayerPrefs.GetInt("JuegoIniciado") == 0));

        btnCreditos.SetActive(false);
        coverEquipo.SetActive(PlayerPrefs.GetInt("JuegoIniciado") == 0);
        
        if (PlayerPrefs.GetInt("JuegoIniciado") == 0)
            Invoke(nameof(GameBootFinished), 3.0f);
        
        if (PlayerPrefs.GetInt("CreditosDesbloqueados") == 1)
            EnableCreditsAccess();
    }

    public void Salir()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
    
    public void SelectorNiveles()
    {
        SceneManager.LoadScene("Selector Niveles");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    private void GameBootFinished()
    {
        PlayerPrefs.SetInt("JuegoIniciado", 1);
        coverEquipo.SetActive(false);
    }

    private void EnableCreditsAccess()
    {
        btnCreditos.SetActive(true);
    }
}
