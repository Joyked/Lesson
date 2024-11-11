using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private Enemy _prefabEnemy;
    [SerializeField] private Hero _hero;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _secondsBeforeAppearance;

    private ObjectPool<Enemy> _poolEnemy;

    private void Awake()
    {
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
    }

    private void Start()
    {
        StartCoroutine(EnemyCycle());
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_prefabEnemy, transform.position, Quaternion.identity);
        enemy.SetDirection(_hero);
        return enemy;
    }

    private void ActionOnGet(Enemy enemy)
    {
        enemy.transform.position = transform.position;
        enemy.SetDirection(_hero);
        enemy.gameObject.SetActive(true);
        enemy.ObjectKilled += ReturnToPool;
    }

    private void ActionOnRelease(Enemy enemy)
    {
        enemy.ObjectKilled -= ReturnToPool;
        enemy.gameObject.SetActive(false);
        _poolEnemy.Get();
    }

    private void ReturnToPool(Enemy enemy)
    {
        _poolEnemy.Release(enemy);
    }
    
    private IEnumerator EnemyCycle()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            yield return new WaitForSeconds(_secondsBeforeAppearance);
            _poolEnemy.Get();
        }
    }
}
