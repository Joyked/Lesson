using UnityEngine;
using System.Collections.Generic;

public class QueueCookies : MonoBehaviour
{
    private Scanner _scanner;
    private Queue<Cookie> _cookies;

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
        _cookies = new Queue<Cookie>();
    }

    private void OnEnable() =>
        _scanner.CookieOnFloor += AddCookie;

    private void OnDisable() =>
        _scanner.CookieOnFloor -= AddCookie;

    public bool HasCookies() =>
        _cookies.Count > 0;

    public Cookie GiveAway() =>
        _cookies.Dequeue();
    
    private void AddCookie(Cookie cookie) =>
        _cookies.Enqueue(cookie);
}
