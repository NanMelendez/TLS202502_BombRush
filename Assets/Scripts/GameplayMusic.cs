using UnityEngine;

public class GameplayMusic : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgMusic;
    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private float loopOfset;

    // TODO: endloop: 1:53

    void Start()
    {
        if (bgMusic != null && clip != null)
        {
            bgMusic.clip = clip;
            bgMusic.Play();
        }

        bgMusic.PlayScheduled(AudioSettings.dspTime + loopOfset);
    }
}
