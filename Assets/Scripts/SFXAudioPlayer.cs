using UnityEngine;

public class SFXAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public AudioClips audioClips;
    public static SFXAudioPlayer Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource is not assigned in SFXAudioPlayer.");
            }
        }
        EventBus.Subscribe<GameEvents.Lost>(PlayLoseGameSound);
        EventBus.Subscribe<GameEvents.Win>(PlayWinGameSound);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned in SFXAudioPlayer.");
        }
    }
    void PlayLoseGameSound(GameEvents.Lost lost)
    {
        PlaySFX(audioClips.loseGameSound);
    }
    void PlayWinGameSound(GameEvents.Win win)
    {
        PlaySFX(audioClips.winGameSound);
    }
    void OnDestroy()
    {
        EventBus.Unsubscribe<GameEvents.Lost>(PlayLoseGameSound);
        EventBus.Unsubscribe<GameEvents.Win>(PlayWinGameSound);
    }
}
