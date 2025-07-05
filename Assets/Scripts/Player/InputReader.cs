using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    public float Direction { get; private set; }

    public event Action SpaceKeyDowned;

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
        
        if (Input.GetKey(KeyCode.Space))
        {
            SpaceKeyDowned?.Invoke();
        }
    }
}
