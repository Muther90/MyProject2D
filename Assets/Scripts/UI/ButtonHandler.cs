using UnityEngine.UI;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    [SerializeField] protected Button _button; 

    public event System.Action OnButtonClicked;

    protected virtual void OnEnable()
    {
        _button.onClick.AddListener(HandleButtonClick);
    }

    protected virtual void OnDisable()
    {
        _button.onClick.RemoveListener(HandleButtonClick);
    }

    protected virtual void HandleButtonClick()
    {
        OnButtonClicked?.Invoke(); 
    }
}
