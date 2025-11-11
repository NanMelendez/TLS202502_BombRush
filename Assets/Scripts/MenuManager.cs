using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject preguntaSalir;

    public void Salir()
    {
        Application.Quit();
    }
    
    public void SelectorNiveles()
    {
        SceneManager.LoadScene("Selector Niveles");
    }
}
