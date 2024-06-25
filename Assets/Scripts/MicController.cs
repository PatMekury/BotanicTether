using UnityEngine;
using UnityEngine.UI;

public class MicController : MonoBehaviour
{
    private AudioClip recordedClip;
    public AudioSource audioSource;
    public Toggle micToggle;
    public AudioClip enabledSound;
    public AudioClip disabledSound;
    private bool isMicEnabled = true;

    void Start()
    {
        // Request microphone permissions (iOS-specific)
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
            {
                Application.RequestUserAuthorization(UserAuthorization.Microphone);
            }
        }

        // Initialize the microphone
        recordedClip = Microphone.Start(null, true, 1, AudioSettings.outputSampleRate);
        Microphone.End(null);

        micToggle.onValueChanged.AddListener(ToggleMic);
    }

    void ToggleMic(bool isOn)
    {
        isMicEnabled = isOn;

        if (isMicEnabled)
        {
            recordedClip = Microphone.Start(null, true, 1, AudioSettings.outputSampleRate);
            audioSource.clip = enabledSound;
        }
        else
        {
            Microphone.End(null);
            audioSource.clip = disabledSound;
        }

        audioSource.Play();
    }
}
