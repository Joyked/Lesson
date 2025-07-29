using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnt : MonoBehaviour
{
    [SerializeField] private List<Collector> _collectors = new List<Collector>();
    [SerializeField] private QueueCookies _cookies;
    public event Action<Cookie> CookieOnBase;

    public int AntCount { get { return _collectors.Count; } private set{} }

    private void Start() =>
        StartCoroutine(SendAntsCoroutine());

    private void OnEnable()
    {
        foreach (var collector in _collectors)
            collector.GotCookie += ReturnAnt;
    }

    private void OnDisable()
    {
        foreach (var collector in _collectors)
            collector.GotCookie -= ReturnAnt;
    }

    public void Create(Collector ant)
    {
        foreach (var collector in _collectors)
            collector.GotCookie -= ReturnAnt;
        
        _collectors = new List<Collector>();
        AddAnt(ant);
    }

    public void AddAnt(Collector ant)
    {
        ant.GotCookie += ReturnAnt;
        _collectors.Add(ant);
    }

    public Collector RemoveAnt()
    {
        if (_collectors.Count > 1)
        {
            Collector collector = _collectors[0];
            _collectors.RemoveAt(0);
            return collector;
        }
        
        return null;
    }


    private IEnumerator SendAntsCoroutine()
    {
        bool isWork = true;
        
        while (isWork)  
        { 
            if (_cookies.HasCookies())
                SendAnt();

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cookie cookie))
            CookieOnBase?.Invoke(cookie);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out AntMover ant))
            ant.Strolle();
    }

    private void ReturnAnt(AntMover ant) =>
        ant.SetTarget(transform);
    
    private void SendAnt()
    {
        for (int i = 0; i < _collectors.Count; i++)
        {
            if (_collectors[i].Ant.IsAvailable)
            {
                Cookie cookie = _cookies.GiveAway();
                _collectors[i].Ant.SetTarget(cookie.transform);
                _collectors[i].SetTargetCookie(cookie);
                break;
            }
        }
    }
}
