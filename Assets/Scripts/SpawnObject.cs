using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Projectile _prefabProjectile;
    [SerializeField] private UFOMover _prefabUFO;
    [SerializeField] private float _speedProjectile;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _secondsBeforeAppearance;

    private ObjectPool<Projectile> _poolEnemy;

    private void Awake()
    {
        _poolEnemy = new ObjectPool<Projectile>
        (
            CreateEnemy,
            ActionOnGet,
            ActionOnRelease,
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        StartCoroutine(ProjectileCycle());
    }
    
    private IEnumerator ProjectileCycle()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_secondsBeforeAppearance);
        
        while (true)
        {
            _poolEnemy.Get();
            yield return waitForSeconds;
        }
    }

    private Projectile CreateEnemy()
    {
        Projectile projectile = Instantiate(_prefabProjectile, transform.position, Quaternion.identity);
        projectile.SetParams(_prefabUFO, _speedProjectile);
        return projectile;
    }

    private void ActionOnGet(Projectile projectile)
    {
        projectile.transform.position = transform.position;
        projectile.SetParams(_prefabUFO, _speedProjectile);
        projectile.gameObject.SetActive(true);
        projectile.HitedTarget += ReturnToPool;
    }

    private void ActionOnRelease(Projectile projectile)
    {
        projectile.HitedTarget -= ReturnToPool;
        projectile.gameObject.SetActive(false);
    }

    private void ReturnToPool(Projectile projectile)
    {
        _poolEnemy.Release(projectile);
    }
}
