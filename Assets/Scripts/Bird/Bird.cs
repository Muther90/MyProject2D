using System;    
using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(BirdCollisionHandler))]
public class Bird : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private InputReader _inputReader;
    private BirdMover _birdMover;
    private BirdCollisionHandler _birdCollisionHandler;

    public event Action Dead;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _birdMover = GetComponent<BirdMover>();
        _birdCollisionHandler = GetComponent<BirdCollisionHandler>();
    }

    private void OnEnable()
    {
        _inputReader.JumpKeyDowned += Jump;
        _inputReader.FireKeyDowned += Fire;
        _birdCollisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _inputReader.JumpKeyDowned -= Jump;
        _inputReader.FireKeyDowned -= Fire;
        _birdCollisionHandler.CollisionDetected -= ProcessCollision;
    }

    public void Reset()
    {
        _birdMover.Reset();
        _scoreCounter.Reset();
        _bulletSpawner.Reset();
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Turret || interactable is Ground || interactable is Bullet)
        {
            Dead?.Invoke();
        }
        else if (interactable is ScoreZone)
        {
            _scoreCounter.Increase();
        }
    }

    private void Jump()
    {
        _birdMover.Jump();
    }

    private void Fire()
    {
        _bulletSpawner.Fire();
    }
}