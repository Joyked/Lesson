using System;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private BaseAnt _baseAnt;
    public int Count { get; private set; }
    
    public event Action<int> Changed;

    private void Awake() =>
        _baseAnt = GetComponent<BaseAnt>();

    private void OnEnable() =>
        _baseAnt.CookieOnBase += Increase;

    private void OnDisable() =>
        _baseAnt.CookieOnBase -= Increase;

    private void Increase(Cookie cookie)
    {
        Count++;
        Changed?.Invoke(Count);
    }

    public void BuyAnt(int price)
    {
        int minPrice = 3;
        
        if (price < minPrice)
            price = minPrice;
        
        Count -= price;
        Changed?.Invoke(Count);
    }

    public void BuyBase(int price)
    {
        int minPrice = 5;
        
        if (price < minPrice)
            price = minPrice;
        
        Count -= price;
        Changed?.Invoke(Count);
    }
}
