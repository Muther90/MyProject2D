using System;
using UnityEngine;

public class StartGameScreen : Window
{
    public event Action PlayButtonClicked;

    public override void Open()
    {
        WindowGroup.alpha = 1f;
        WindowGroup.interactable = true;
        WindowGroup.blocksRaycasts = true;
    }

    public override void Close()
    {
        WindowGroup.alpha = 0f;
        WindowGroup.interactable = false;
        WindowGroup.blocksRaycasts = false;
    }

    protected override void OnButtonClick()
    {
        PlayButtonClicked?.Invoke();
    }
}
