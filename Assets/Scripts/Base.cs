using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private AntMover[] _ants;
    [SerializeField] private Scanner _scanner;

    private Queue<Cookie> _cookies;
    
    public event Action<Cookie> CookieOnBase;

    private void Awake() =>
        _cookies = new Queue<Cookie>();

    private void OnEnable() =>
        _scanner.CookieOnFloor += AddCookie;

    private void OnDisable() =>
        _scanner.CookieOnFloor -= AddCookie;

    private void Update()
    {
        if (_cookies.Count > 0)
            SendAnt();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cookie cookie))
            CookieOnBase?.Invoke(cookie);
    }

    private void AddCookie(Cookie cookie) =>
        _cookies.Enqueue(cookie);
    
    private void SendAnt()
    {
        for (int i = 0; i < _ants.Length; i++)
        {
            if (_ants[i].IsAvailable)
            {
                _ants[i].SetTarget(_cookies.Dequeue());
                break;
            }
        }
    }
}
