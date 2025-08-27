using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score = 0;

    public event Action<int> ScoreChanged;

    public void Reset()
    {
        ScoreChanged?.Invoke(_score = 0);
    }

    public void Increase()
    {
        ScoreChanged?.Invoke(++_score);
    }
}