using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const KeyCode SpaceKey = KeyCode.Space;
    private const KeyCode FKey = KeyCode.F;

    public event Action JumpKeyDowned;
    public event Action FireKeyDowned;

    private void Update()
    {
        if (Input.GetKeyDown(SpaceKey))
        {
            JumpKeyDowned?.Invoke();
        }

        if (Input.GetKeyDown(FKey))
        {
            FireKeyDowned?.Invoke();
        }
    }
}