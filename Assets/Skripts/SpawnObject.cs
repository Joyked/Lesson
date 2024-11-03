using System;
using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Enemy _prefabEnemy;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _secondsBeforeAppearance;

    private ObjectPool<Enemy> _poolEnemy;
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        
        _poolEnemy = new ObjectPool<Enemy>
        (
            CreateEnemy,
            ActionOnGet,
            ActionOnRelease,
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );

        _poolEnemy.Get();
    }

    private void Start()
    {
        StartCoroutine(EnemyCycle());
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_prefabEnemy, GetDirection(), Quaternion.identity);
        enemy.SetDirection(new Vector3(0, Random.Range(-180, 180), 0));
        return enemy;
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.transform.position = GetDirection();
        enemy.SetDirection(new Vector3(0, Random.Range(-180, 180), 0));
        enemy.gameObject.SetActive(true);
        enemy.ObjectFelled += ReturnToPool;
    }

    private void ActionOnRelease(Enemy enemy)
    {
        enemy.ObjectFelled -= ReturnToPool;
        enemy.gameObject.SetActive(false);
    }

    private void ReturnToPool(Enemy enemy)
    {
        _poolEnemy.Release(enemy);
    }
    
    private IEnumerator EnemyCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(_secondsBeforeAppearance);
            _poolEnemy.Get();
        }
    }
    
    private Vector3 GetDirection()
    {
        Bounds bounds = _collider.bounds;
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), transform.position.y,
            Random.Range(bounds.min.z, bounds.max.z));
    }
}
