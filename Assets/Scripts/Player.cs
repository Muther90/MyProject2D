using UnityEngine;

public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private Mover _mover;
    private Health _health;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<Mover>();
        _health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
        _mover.Move(_inputReader.Direction);
    }

    public void ApplyHeal(float amount) => _health.ApplyHeal(amount);
    public void ApplyDamage(float amount) => _health.ApplyDamage(amount);
}
