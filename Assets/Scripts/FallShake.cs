using Unity.Cinemachine;
using UnityEngine;

public class FallShake : MonoBehaviour
{
    [SerializeField]
    private CinemachineBasicMultiChannelPerlin shakeNoise;
    private float shakeTimer;
    private float shakeTimerTotal;
    private float startingIntensity;

    void Update()
    {
        // Debug.Log(fallSpeed);
        if (shakeTimer > 0.0f)
        {
            Debug.Log(shakeTimer);
            shakeTimer -= Time.deltaTime;
            shakeNoise.AmplitudeGain = Mathf.Lerp(startingIntensity, 0.0f, shakeTimer / shakeTimerTotal);
        }
    }

    public void ShakeCamera(float intensity, float time)
    {
        shakeNoise.AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;
    }
}
