using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject btnBase;
    [SerializeField]
    private GameObject btnLevel;
    [SerializeField]
    private int margin;
    private int levelCount;
    private RectTransform btnTransform;

    void Start()
    {
        levelCount = SceneManager.sceneCountInBuildSettings - 3;
        btnTransform = btnLevel.GetComponent<RectTransform>();

        Debug.Log("Conteo de niveles: " + levelCount);

        for (int i = 0; i < levelCount; i++)
        {
            GameObject newBtn = Instantiate(btnLevel, btnBase.transform.position + i * new Vector3(btnTransform.rect.width + margin, 0), Quaternion.identity);
            newBtn.transform.SetParent(btnBase.transform, false);

            Button btn = newBtn.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => { GoToLevel(i + 1);});

            TextMeshProUGUI txt = newBtn.GetComponentInChildren<TextMeshProUGUI>();
            txt.text = (i + 1).ToString();
        }
    }

    public void RegresarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void GoToLevel(int n)
    {
        SceneManager.LoadScene("Nivel " + n);
    }
}
