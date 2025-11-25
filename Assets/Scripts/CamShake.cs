using Unity.Cinemachine;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField]
    private CinemachineBasicMultiChannelPerlin shakeNoise;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;
    // private float finalFrequency;
    // private float shakeFrequency;

    void Update()
    {
        if (shakeTimer > 0.0f)
        {
            shakeNoise.AmplitudeGain = Mathf.Lerp(0.0f, startingIntensity, shakeTimer / shakeTimerTotal);
            // shakeNoise.FrequencyGain = Mathf.Lerp(, shakeFrequency / finalFrequency);
            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCamera(float intensity /*, float freq*/, float time)
    {
        startingIntensity = intensity;
        // finalFrequency = freq;
        // shakeFrequency = 0.0f;
        shakeTimerTotal = time;
        shakeTimer = time;
    }
}
