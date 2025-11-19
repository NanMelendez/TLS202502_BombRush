using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject preguntaSalir;

    public void Salir()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = true;
#endif
        Application.Quit();
    }
    
    public void SelectorNiveles()
    {
        SceneManager.LoadScene("Selector Niveles");
    }
}
