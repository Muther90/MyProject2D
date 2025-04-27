using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterAudioToggle : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    public bool IsAudioEnabled { get; private set; } = true;

    private void OnEnable()
    {
        _button.onClick.AddListener(ToggleAudio);
        EnableAudio();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ToggleAudio);
    }

    private void ToggleAudio()
    {
        if (IsAudioEnabled)
        {
            DisableAudio();
        }
        else
        {
            EnableAudio();
        }
    }

    public void EnableAudio()
    {
        IsAudioEnabled = true;
        AudioListener.pause = false;
    }

    public void DisableAudio()
    {
        IsAudioEnabled = false;
        AudioListener.pause = true;
    }
}
