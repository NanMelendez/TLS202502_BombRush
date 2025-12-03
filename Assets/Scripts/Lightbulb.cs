using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Ligthbulb : MonoBehaviour
{
    public bool switchOn;
    [SerializeField]
    private Sprite imgOn;
    [SerializeField]
    private Sprite imgOff;
    [SerializeField]
    private Material matOn;
    [SerializeField]
    private Material matOff;
    [SerializeField]
    private SpriteRenderer lightRenderer;
    [SerializeField]
    private Light2D lightSrc;
    private bool prevSwitchState;

    void Start()
    {
        ToggleLights();
    }

    void Update()
    {
        if (switchOn != prevSwitchState)
            ToggleLights();
    }

    private void ToggleLights()
    {
        lightRenderer.sprite = switchOn ? imgOn : imgOff;
        lightRenderer.material = switchOn ? matOn : matOff;
        lightSrc.enabled = switchOn;
        prevSwitchState = switchOn;
    }
}
