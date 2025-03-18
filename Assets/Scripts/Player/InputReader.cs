using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = "Horizontal";

    private bool _isJump;
    private bool _isHit;

    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
    
        if (Input.GetKeyDown(KeyCode.W))
        {
            _isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isHit = true;
        }
    }

    public bool GetIsJump()
    {
        return GetBoolAsTrigger(ref _isJump);
    }

    public bool GetIsHit()
    {
        return GetBoolAsTrigger(ref _isHit);
    }

    private bool GetBoolAsTrigger(ref bool flag)
    {
        bool value = flag;
        flag = false;

        return value;
    }
}
