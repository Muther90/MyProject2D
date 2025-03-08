using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoints;

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _targetPoints = new Transform[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            _targetPoints[i] = transform.GetChild(i);
        }
    }
#endif

    public Vector2[] Get()
    {
        if (_targetPoints == null || _targetPoints.Length == 0)
        {
            throw new System.Exception("Targets is not assigned!");
        }

        Vector2[] waypoints = new Vector2[_targetPoints.Length];

        for (int i = 0; i < _targetPoints.Length; i++)
        {
            waypoints[i] = _targetPoints[i].position;
        }

        return waypoints;
    }
}
