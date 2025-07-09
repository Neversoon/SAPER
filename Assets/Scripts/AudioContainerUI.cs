using UnityEngine;
using UnityEngine.UI;

public class AudioContainerUI : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Button muteButton;
    [SerializeField] Image muteButtonImage;
    [SerializeField] Slider volumeSlider;

    void Awake()
    {
        muteButton.onClick.AddListener(AudioMuteSwitch);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    void Start()
    {
        volumeSlider.value = audioSource.volume;
    }

    public void AudioMuteSwitch()
    {
        if (audioSource != null)
        {
            audioSource.mute = !audioSource.mute;
            UpdateMuteButtonImage();
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned in AudioContainerUI.");
        }
    }

    private void UpdateMuteButtonImage()
    {
        muteButtonImage.gameObject.SetActive(audioSource.mute);
    }
    public void ChangeVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned in AudioContainerUI.");
        }
    }
}
