using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(InputReader), typeof(Mover))]
public class Player : MonoBehaviour, IHealth
{
    [SerializeField] Health _health;
    [SerializeField] InputReader _inputReader;
    [SerializeField] Mover _mover;

    private void FixedUpdate()
    {
        _mover.Move(_inputReader.Direction);
    }

    public void TakeDamage(float damage) => _health.ApplyDamage(damage);
    public void TakeHeal(float heal) => _health.ApplyHeal(heal);
}
