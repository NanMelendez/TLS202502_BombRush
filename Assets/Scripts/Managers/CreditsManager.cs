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
    [SerializeField]
    private PersistentData gameData;

    public float scrollDuration;
    private float scrollTimer = 0.0f;

    void Start()
    {
        skipCredits.SetActive(gameData.areCreditsUnlocked);
    }

    void Update()
    {
        if (scrollTimer < scrollDuration)
        {
            creditsBase.transform.position = Vector3.Lerp(first.transform.position, last.transform.position, scrollTimer / scrollDuration);
            scrollTimer += Time.deltaTime;
        }

        if (!gameData.areCreditsUnlocked && scrollTimer >= scrollDuration)
        {
            gameData.areCreditsUnlocked = true;
            SkipToMenu();
        }
    }

    public void SkipToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
