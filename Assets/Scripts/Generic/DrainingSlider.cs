using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrainingSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    /// <summary>
    /// ������� �������� �� �������� �����������������.
    /// </summary>
    /// <param name="duration">����� � �������� ��� ������� ����������� ��������.</param>
    public IEnumerator Empty(float duration)
    {
        // ������������� �������� �������� � ������������
        _slider.value = _slider.maxValue;

        // ���� �������� �� ��������� �� ������������ ��������
        while (_slider.value > _slider.minValue)
        {
            // ������ ��������� �������� ��������
            _slider.value = Mathf.MoveTowards(
                _slider.value,
                _slider.minValue,
                Time.deltaTime / duration * (_slider.maxValue - _slider.minValue)
            );

            yield return null;
        }
    }
}
