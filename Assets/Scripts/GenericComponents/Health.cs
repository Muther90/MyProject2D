using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _hitPoint;

    public void TakeHeal(int amount)
    {
        _hitPoint += amount;
    }

    public void TakeDamage(int amount)
    {
        _hitPoint -= amount;

        if (_hitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}

