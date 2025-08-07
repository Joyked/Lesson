using UnityEngine;
using System;

public class Scanner : MonoBehaviour
{ 
    public event Action<Cookie> CookieOnFloor;
    public event Action<Cookie> CookieWereTaken;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cookie cookie))
            CookieOnFloor?.Invoke(cookie);
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cookie cookie))
            CookieWereTaken?.Invoke(cookie);
    }
}
