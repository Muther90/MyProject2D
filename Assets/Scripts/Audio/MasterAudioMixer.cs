using UnityEngine;
using UnityEngine.Audio;

public class MasterAudioMixer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private AudioToggleButton _audioToggleButton;

    private void OnEnable()
    {
        _audioToggleButton.OnButtonClicked += ToggleAudio;

        AudioData.EnableAudio();
    }

    private void OnDisable()
    {
        _audioToggleButton.OnButtonClicked -= ToggleAudio;

    }

    private void ToggleAudio()
    {
        if (AudioData.IsAudioEnabled)
        {
            AudioData.DisableAudio();
        }
        else
        {
            AudioData.EnableAudio();
        }

        AudioListener.pause = !AudioData.IsAudioEnabled;
    }
}
