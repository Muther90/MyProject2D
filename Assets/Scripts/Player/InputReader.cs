using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public static event Action SpaceKeyDowned;

    public const string Horizontal = "Horizontal";

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
        
        if (Input.GetKey(KeyCode.Space))
        {
            SpaceKeyDowned.Invoke();
        }
    }
}
