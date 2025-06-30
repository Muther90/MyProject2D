using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public IEnumerator Filling(float duration)
    {
        _slider.value = _slider.minValue;

        yield return MoveTo(_slider.maxValue, duration);
    }

    public IEnumerator Draining(float duration)
    {
        _slider.value = _slider.maxValue;

        yield return MoveTo(_slider.minValue, duration);
    }

    private IEnumerator MoveTo(float toValue, float duration)
    {
        if (IsPositiveValue(duration))
        {
            while (Mathf.Approximately(_slider.value, toValue) == false)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, toValue, Time.deltaTime / duration);

                yield return null;
            }
        }
    }

    private bool IsPositiveValue(float value)
    {
        return value > 0f;
    }
}
