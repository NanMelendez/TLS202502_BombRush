using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderLogic : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;
    [SerializeField]
    private TextMeshProUGUI text;

    public float Value
    {
        get
        {
            return slider.value;
        }
        set
        {
            slider.value = value;
            fill.color = gradient.Evaluate(slider.normalizedValue);
            SetText();
        }
    }

    public float MaxValue
    {
        get
        {
            return slider.maxValue;
        }
        set
        {
            slider.maxValue = value;
            slider.value = value;
            fill.color = gradient.Evaluate(1.0f);
            SetText();
        }
    }

    private void SetText()
    {
        if (text != null)
        {
            if (CompareTag("Time"))
            {
                int minutes = Mathf.FloorToInt(Value / 60.0f);
                int seconds = Mathf.FloorToInt(Value % 60.0f);
                text.text = string.Format("{0:0}:{1:00}", minutes, seconds);
                return;
            }

            text.text = Value.ToString();
        }
    }
}
