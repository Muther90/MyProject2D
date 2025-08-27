using UnityEngine;

public class BirdTracker : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private float _xOffset;

    private Transform _birdTransform;
    private Vector3 _currentPosition;

    private void Awake()
    {
        _birdTransform = _bird.transform;
    }

    private void LateUpdate()
    {
        _currentPosition = transform.position;
        _currentPosition.x = _birdTransform.position.x + _xOffset;
        transform.position = _currentPosition;
    }
} 