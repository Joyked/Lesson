using System;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private AntBase _antBase;
    
    public int Count { get; private set; }
    
    public event Action Changed;

    private void Awake() =>
        _antBase = GetComponent<AntBase>();

    private void OnEnable() =>
        _antBase.CookieOnBase += Increase;

    private void OnDisable() =>
        _antBase.CookieOnBase -= Increase;

    private void Increase(Cookie cookie)
    {
        ++Count;
        Changed?.Invoke();
    }

    public void GiveAway(int price)
    {
        if (price < 1)
            price *= -1;
        
        Count -= price;
        Changed?.Invoke();
    }
}
