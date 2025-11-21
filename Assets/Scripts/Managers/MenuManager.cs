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
    [SerializeField]
    private PersistentData gameData;

    void Start()
    {
        btnCreditos.SetActive(gameData.areCreditsUnlocked);
        coverEquipo.SetActive(!gameData.gameIsBooted);

        if (!gameData.gameIsBooted)
            Invoke(nameof(GameBootFinished), 3.0f);
        
        if (gameData.areCreditsUnlocked)
            EnableCreditsAccess();
    }

    public void Salir()
    {
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
        gameData.gameIsBooted = true;
        coverEquipo.SetActive(false);
    }

    private void EnableCreditsAccess()
    {
        btnCreditos.SetActive(true);
    }
}
