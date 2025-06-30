using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrainingSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    /// <summary>
    /// Пустеет ползунок за заданную продолжительность.
    /// </summary>
    /// <param name="duration">Время в секундах для полного опустошения ползунка.</param>
    public IEnumerator Empty(float duration)
    {
        // Устанавливаем значение ползунка в максимальное
        _slider.value = _slider.maxValue;

        // Пока ползунок не опустится до минимального значения
        while (_slider.value > _slider.minValue)
        {
            // Плавно уменьшаем значение ползунка
            _slider.value = Mathf.MoveTowards(
                _slider.value,
                _slider.minValue,
                Time.deltaTime / duration * (_slider.maxValue - _slider.minValue)
            );

            yield return null;
        }
    }
}
