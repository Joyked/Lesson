using System;
using System.Collections;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Collector[] _collectors;
    [SerializeField]private QueueCookies _cookies;
    public event Action<Cookie> CookieOnBase;
    
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

    private IEnumerator SendAntsCoroutine()
    {
        bool work = true;
        
        while (work)
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
        for (int i = 0; i < _collectors.Length; i++)
        {
            if (_collectors[i].Ant.IsAvailable)
            {
                Cookie cookie = _cookies.GiveAway();
                _collectors[i].SetTargetCookie(cookie);
                _collectors[i].Ant.SetTarget(cookie.transform);
                break;
            }
        }
    }
}
