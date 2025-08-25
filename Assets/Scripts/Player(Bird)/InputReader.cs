using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action SpaceKeyDowned;
    public event Action FKeyDowned;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceKeyDowned?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FKeyDowned?.Invoke();
        }
    }
}