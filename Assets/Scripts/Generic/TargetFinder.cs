using System.Linq;
using UnityEngine;

public static class TargetFinder 
{
    private static readonly Collider2D[] _overlapColliders = new Collider2D[100];

    public static IDamageable FindNearestDamageable(Transform centerTransform, float radius)
    {
        Vector2 centerPosition = centerTransform.position;
        int countColliders = Physics2D.OverlapCircleNonAlloc(centerPosition, radius, _overlapColliders);

        if (countColliders > 0)
        {
            return _overlapColliders
            .Take(countColliders)
            .Select(collider =>
                collider.TryGetComponent<IDamageable>(out IDamageable damageableTarget)
                ? damageableTarget
                : null)
            .Where(damageableTarget =>
                damageableTarget != null)
            .OrderBy(damageableTarget =>
                ((Vector2)((Component)damageableTarget).transform.position - centerPosition).sqrMagnitude)
            .FirstOrDefault();
        }
        else 
        {
            return null;
        }
    }
}
