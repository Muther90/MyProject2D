using UnityEngine;

public class BulletSpawner : BaseObjectPool<Bullet>
{
    [SerializeField] Transform _spawnPoint;

    public void Fire()
    {
        Spawn(_spawnPoint.position, _spawnPoint.rotation);
    }
}