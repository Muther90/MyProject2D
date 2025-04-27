using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public abstract class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AudioMixerGroup _audioMixerGroup;
    [SerializeField] private MasterAudioToggle _masterAudioToggle;

    protected abstract string VolumeParametr { get; }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleValueChanged);
    }

    private void HandleValueChanged(float value)
    {
        if (_masterAudioToggle.IsAudioEnabled)
        {
            _audioMixerGroup.audioMixer.SetFloat(VolumeParametr, value > 0 ? Mathf.Log10(value) * AudioData.DBMultiplier : AudioData.MinVolume);
        }
    }
}
