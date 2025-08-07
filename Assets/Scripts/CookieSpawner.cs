using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CookieSpawner : MonoBehaviour
{
    [SerializeField] private Cookie _cookiePrefab;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private List<AntBase> _antBase;

    private ObjectPool<Cookie> _pool;
    private List<Cookie> _cookies;
        
    private void Awake()
    {
        _cookies = new List<Cookie>();
        
        _pool = new ObjectPool<Cookie>
        (
            CreateCookie,
            ActionOnGet,
            ActionOnRelease,
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
        
        for (int i = 0; i < _poolCapacity; i++)
            _pool.Get();
    }

    private void OnEnable()
    {
        foreach (var baseAnt in _antBase)
            baseAnt.CookieOnBase += _pool.Release;
    }

    private void OnDisable()
    {
        foreach (var baseAnt in _antBase)
            baseAnt.CookieOnBase -= _pool.Release;
    }

    public void AddNewBase(AntBase antBase)
    {
        _antBase.Add(antBase);
        antBase.CookieOnBase += _pool.Release;
    }

    private Cookie CreateCookie()
    {
        var cookie = Instantiate(_cookiePrefab);
        _cookies.Add(cookie);
        return cookie;
    }

    private void ActionOnGet(Cookie cookie)
    {
        float minPosition = -7;
        float maxPosition = 8;
        float xPosition = Random.Range(minPosition, maxPosition);
        float zPosition = Random.Range(minPosition, maxPosition);
        float yPosition = 10;
        
        cookie.transform.position = new Vector3(xPosition, yPosition, zPosition);
        Collider collider = cookie.GetComponent<Collider>();
        collider.isTrigger = false;
        cookie.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Cookie cookie)
    {
        cookie.gameObject.SetActive(false);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        float secondDelay = 2;
        yield return new WaitForSeconds(secondDelay);
        _pool.Get();
    }
}
