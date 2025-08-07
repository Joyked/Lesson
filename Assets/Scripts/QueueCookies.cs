using UnityEngine;
using System.Collections.Generic;

public class QueueCookies : MonoBehaviour
{
    private Scanner _scanner;
    private List<Cookie> _cookies;
    private int _index;

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();
        _cookies = new List<Cookie>();
    }

    private void OnEnable()
    {
        _scanner.CookieOnFloor += AddCookie;
        _scanner.CookieWereTaken += RemoveCookie;
    }

    private void OnDisable()
    {
        _scanner.CookieOnFloor -= AddCookie;
        _scanner.CookieWereTaken -= RemoveCookie;
    }

    public bool HasCookies() =>
        _cookies.Count > 0;

    public Cookie GiveAway()
    {
        _index = (_index + 1) % _cookies.Count;
        Cookie cookie = _cookies[_index];
        return cookie;
    }
    
    public void AddCookie(Cookie cookie) =>
        _cookies.Add(cookie);

    private void RemoveCookie(Cookie cookie) =>
        _cookies.Remove(cookie);
}
