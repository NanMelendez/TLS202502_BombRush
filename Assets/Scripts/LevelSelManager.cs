using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelManager : MonoBehaviour
{
    public void RegresarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
