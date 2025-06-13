using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Router), typeof(Mover))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;
    [SerializeField] private Mover _mover;
    [SerializeField] private Router _router;

    private void FixedUpdate()
    {
        _mover.Move(Mathf.Sign(_router.CurrentTarget.x - transform.position.x));
    }

    public void TakeDamage(float damage) => _health.ApplyDamage(damage);
}
