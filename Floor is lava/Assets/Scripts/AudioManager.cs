using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    [SerializeField, Range(0,1)]
    float volume = 0.5f;
    public bool isMuted;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isMuted = false;
        audioSource.volume = volume;
    }

    public void Mute()
    {
        if (!isMuted)
        {
            audioSource.volume = 0;
            isMuted = true;
        }
    }

    public void UnMute()
    {
        if (isMuted)
        {
            audioSource.volume = volume;
            isMuted = false;
        }
    }
}
