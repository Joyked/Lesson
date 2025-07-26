using UnityEngine;
using System;

public class Scanner : MonoBehaviour
{ 
    public event Action<Cookie> CookieOnFloor;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cookie cookie))
            CookieOnFloor?.Invoke(cookie);
    }
}
