using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Counter))]
public class AntBase : MonoBehaviour
{
    [SerializeField] private List<Collector> _collectors = new List<Collector>();
    [SerializeField] private QueueCookies _cookies;
    [SerializeField] private AntSpawner _antSpawner;
    [SerializeField] private Flag _flag;

    private int _indexCollectors;
    private BaseSpawner _baseSpawner;
    private Counter _counter;
    public Flag Flag => _flag;
    
    public event Action<Cookie> CookieOnBase;

    private void Awake()
    {
        _counter = GetComponent<Counter>();
        _baseSpawner = _antSpawner.GetComponent<BaseSpawner>();
    }

    private void Start() =>
        StartCoroutine(SendAntsCoroutine());

    private void OnEnable()
    {
        _counter.Changed += BuyNewAnt;
        _counter.Changed += BuyNewBase;
        
        foreach (var collector in _collectors)
            collector.GotCookie += ReturnAnt;
    }

    private void OnDisable()
    {
        _counter.Changed -= BuyNewAnt;
        _counter.Changed -= BuyNewBase;
        
        foreach (var collector in _collectors)
            collector.GotCookie -= ReturnAnt;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cookie cookie))
            CookieOnBase?.Invoke(cookie);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out AntMover ant))
            ant.Strolle();
    }

    public void Initialize(QueueCookies cookies, BaseSpawner baseSpawner)
    {
        _cookies = cookies;
        _antSpawner = baseSpawner.GetComponent<AntSpawner>();
        _baseSpawner = baseSpawner;
    }


    public void AddAnt(Collector ant)
    {
        _collectors.Add(ant);
        ant.GotCookie += ReturnAnt;
    }

    public Collector RemoveAnt()
    {
        if (_collectors.Count > 1)
        {
            Collector collector = _collectors[0];
            _collectors.RemoveAt(0);
            collector.GotCookie -= ReturnAnt;
            return collector;
        }
        
        return null;
    }
    
    private IEnumerator SendAntsCoroutine()
    {
        bool isWork = true;
    
        while (isWork)  
        { 
            while (_cookies.HasCookies() && HasAvailableAnts())
                SendAnt();
            
            yield return null;
        }
    }

    private bool HasAvailableAnts()
    {
        foreach (var collector in _collectors)
        {
            if (collector.Ant.IsAvailable)
                return true;
        }
        return false;
    }

    private void SendAnt()
    {
        Cookie cookie = _cookies.GiveAway();
        Collector collector = _collectors[_indexCollectors];
        _indexCollectors = (_indexCollectors + 1) % _collectors.Count;
                
        collector.Ant.SetTarget(cookie.transform);
        collector.SetTargetCookie(cookie, this);
    }

    private void ReturnAnt(AntMover ant) =>
        ant.SetTarget(transform);

    private void BuyNewAnt()
    {
        if(Flag.IsPosition == false)
            _antSpawner.SpawnNewAnt(this, _counter);
    }

    private void BuyNewBase()
    {
        if (_collectors.Count >= 1)
            _baseSpawner.CreateNewBase(Flag, _counter);
    }
}
