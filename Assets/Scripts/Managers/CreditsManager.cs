using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsBase;
    [SerializeField]
    private Transform first;
    [SerializeField]
    private Transform last;
    [SerializeField]
    private GameObject skipCredits;

    public float scrollDuration;
    private float scrollTimer;

    void Start()
    {
        skipCredits.SetActive(false);

        Time.timeScale = 1.0f;
        scrollTimer = 0.0f;

        if (PlayerPrefs.GetInt("CreditosDesbloqueados") != 1)
            PlayerPrefs.SetInt("CreditosDesbloqueados", 1);
        else
            skipCredits.SetActive(true);
    }

    void Update()
    {
        if (scrollTimer < scrollDuration)
        {
            creditsBase.transform.position = Vector3.Lerp(first.transform.position, last.transform.position, scrollTimer / scrollDuration);
            scrollTimer += Time.deltaTime;
        }

        if (scrollTimer >= scrollDuration)
            SkipToMenu();
    }

    public void SkipToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
