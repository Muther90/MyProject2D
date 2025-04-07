using UnityEngine.UI;
using UnityEngine;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] protected Slider _slider;

    public event System.Action<float> OnValueChanged; 

    protected virtual void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleValueChanged);
    }

    protected virtual void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleValueChanged);
    }

    protected virtual void HandleValueChanged(float value)
    {
        OnValueChanged?.Invoke(value); 
    }
}
