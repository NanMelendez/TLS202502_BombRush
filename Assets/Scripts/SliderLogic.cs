using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderLogic : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI tmpText;

    public void SetMaxValue(float mxVal)
    {
        slider.maxValue = mxVal;
        slider.value = mxVal;
        fill.color = gradient.Evaluate(1.0f);
        SetText();
    }

    public void SetValue(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        SetText();
    }

    private void SetText()
    {
        if (tmpText != null)
        {
            if (CompareTag("Energy"))
                tmpText.text = string.Format("{0}%", Mathf.Round(slider.value));
            else if (CompareTag("Time"))
            {
                int minutes = Mathf.FloorToInt(slider.value / 60.0f);
                int seconds = Mathf.FloorToInt(slider.value % 60.0f);
                tmpText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            }
            else
                tmpText.text = slider.value.ToString();
        }
    }
}
