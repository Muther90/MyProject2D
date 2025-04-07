using UnityEngine.Audio;
using UnityEngine;

public abstract class VolumeSlider : SliderHandler
{
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    protected abstract string VolumeParametr { get; }

    protected override void HandleValueChanged(float value)
    {
        if (AudioData.IsAudioEnabled)
        {
            _audioMixerGroup.audioMixer.SetFloat(VolumeParametr, value > 0 ? Mathf.Log10(value) * AudioData.Params.DBMultiplier : AudioData.Params.MinVolume);
        }
    }
}
