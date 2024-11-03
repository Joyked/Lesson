using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Enemy _prefabEnemy;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _secondsBeforeAppearance;

    private ObjectPool<Enemy> _poolEnemy;
    private List<Enemy> _listEnemy;
    private Collider _collider;

    private void Awake()
    {
        _listEnemy = new List<Enemy>();
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

    private void OnDisable()
    {
        foreach (var enemy in _listEnemy)
            enemy.ObjectFell -= ReturnToPool;
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_prefabEnemy, GiveCoordinates(), new Quaternion(0, Random.Range(0, 360), 0, 0));
        _listEnemy.Add(enemy);
        enemy.ObjectFell += ReturnToPool;
        return enemy;
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.transform.position = GiveCoordinates();
        enemy.transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
        enemy.gameObject.SetActive(true);
        StartCoroutine(EnemyCycle());
    }

    private void ActionOnRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void ReturnToPool(Enemy enemy)
    {
        _poolEnemy.Release(enemy);
    }
    
    private IEnumerator EnemyCycle()
    {
        yield return new WaitForSeconds(_secondsBeforeAppearance);
        _poolEnemy.Get();
    }
    
    private Vector3 GiveCoordinates()
    {
        Bounds bounds = _collider.bounds;
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), transform.position.y,
            Random.Range(bounds.min.z, bounds.max.z));
    }
}
