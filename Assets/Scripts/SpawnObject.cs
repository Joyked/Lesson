using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using UnityEngine.Serialization;

public class SpawnObject : MonoBehaviour
{
    [FormerlySerializedAs("_prefabEnemy")] [SerializeField] private EnemyMover _prefabEnemyMover;
    [FormerlySerializedAs("_hero")] [SerializeField] private HeroMover _heroMover;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private float _secondsBeforeAppearance;

    private ObjectPool<EnemyMover> _poolEnemy;

    private void Awake()
    {
        _poolEnemy = new ObjectPool<EnemyMover>
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
    
    private IEnumerator EnemyCycle()
    {
        for (int i = 0; i < _poolCapacity; i++)
        {
            yield return new WaitForSeconds(_secondsBeforeAppearance);
            _poolEnemy.Get();
        }
    }

    private EnemyMover CreateEnemy()
    {
        EnemyMover enemyMover = Instantiate(_prefabEnemyMover, transform.position, Quaternion.identity);
        enemyMover.SetDirection(_heroMover);
        return enemyMover;
    }

    private void ActionOnGet(EnemyMover enemyMover)
    {
        enemyMover.transform.position = transform.position;
        enemyMover.SetDirection(_heroMover);
        enemyMover.gameObject.SetActive(true);
        enemyMover.Killed += ReturnToPool;
    }

    private void ActionOnRelease(EnemyMover enemyMover)
    {
        enemyMover.Killed -= ReturnToPool;
        enemyMover.gameObject.SetActive(false);
        _poolEnemy.Get();
    }

    private void ReturnToPool(EnemyMover enemyMover)
    {
        _poolEnemy.Release(enemyMover);
    }
}
