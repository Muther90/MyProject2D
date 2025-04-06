using UnityEngine;
using UnityEngine.Audio;

public class MasterAudioMixer : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    private bool _isAudioEnabled; 

    private void OnEnable()
    {
        _audioMixerGroup.audioMixer.SetFloat(AudioData.Params.MasterVolume, 0f);
        _isAudioEnabled = true;
    }

    public void ChangeMasterVolume(float volume) => SetVolume(AudioData.Params.MasterVolume, volume);
    public void ChangeBackgroundVolume(float volume) => SetVolume(AudioData.Params.BackgroundVolume, volume);
    public void ChangeEffectsVolume(float volume) => SetVolume(AudioData.Params.EffectsVolume, volume);

    public void ToggleAudio()
    {
        _isAudioEnabled = !_isAudioEnabled;
        float volume = _isAudioEnabled ? 0f : -80f;

        _audioMixerGroup.audioMixer.SetFloat(AudioData.Params.MasterVolume, volume);
    }
    private void SetVolume(string volumeParam, float volume)
    {
        _audioMixerGroup.audioMixer.SetFloat(volumeParam, Mathf.Log10(volume) * 20);
    }
}
