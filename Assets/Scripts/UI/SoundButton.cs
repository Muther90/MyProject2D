using UnityEngine;

public class SoundButton : ButtonHandler
{
    [SerializeField] private AudioSource _audioSource;

    protected override void HandleButtonClick()
    {
        _audioSource.Play();
    }
}
