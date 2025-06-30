using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speedX;
    [SerializeField] private Rigidbody2D _rigidbody;

    public void Move(float direction)
    {
        _rigidbody.velocity = new Vector2(_speedX * direction, _rigidbody.velocity.y);
    }
}
