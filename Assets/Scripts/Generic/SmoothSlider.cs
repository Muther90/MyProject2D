using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Coroutine _moveToCoroutine;

    public void Filling(float duration)
    {
        _slider.value = _slider.minValue;
        _moveToCoroutine = StartCoroutine(MoveToCoroutine(_slider.maxValue, duration));
    }

    public void Draining(float duration)
    {
        _slider.value = _slider.maxValue;
        _moveToCoroutine = StartCoroutine(MoveToCoroutine(_slider.minValue, duration));
    }

    private IEnumerator MoveToCoroutine(float toValue, float duration)
    {
        if (IsPositiveValue(duration))
        {
            while (Mathf.Approximately(_slider.value, toValue) == false)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, toValue, Time.deltaTime / duration);

                yield return null;
            }
        }

        _moveToCoroutine = null;
    }

    private bool IsPositiveValue(float value)
    {
        return value > 0f;
    }
}
