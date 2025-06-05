using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonHandler : MonoBehaviour
{
    [SerializeField] protected Button _button;
    [SerializeField] protected Health _health;

    protected virtual void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    protected virtual void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    protected virtual void OnClick()
    {
        if (_health != null)
        {
            ApplyEffect();
        }
    }

    protected abstract void ApplyEffect();

}
