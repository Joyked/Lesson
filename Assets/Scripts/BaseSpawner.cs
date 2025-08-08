using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private AntBase _antBasePrefab;
    [SerializeField] private int _price;
    [Space] 
    [SerializeField] private QueueCookies _cookies;
    [SerializeField] private List<AntBase> _bases;
    [SerializeField] private FlagTransporter _flagTransporter;

    private AntSpawner _antSpawner;

    private void OnEnable()
    {
        foreach (var antBase in _bases)
            antBase.PurchasedNewBase += CreateNewBase;
    }

    private void OnDisable()
    {
        foreach (var antBase in _bases)
            antBase.PurchasedNewBase -= CreateNewBase;
    }

    public void CreateNewBase(Counter counter, Flag flag)
    {
        if (counter.Count >= _price)
        {
            var newBase = Instantiate(_antBasePrefab);
            newBase.Initialize(_cookies,  this);
            newBase.transform.position = flag.transform.position;
            AntBase oldBase = counter.GetComponent<AntBase>();
            newBase.AddAnt(oldBase.RemoveAnt());
            _flagTransporter.ReturnOnBase(oldBase);
            counter.GiveAway(_price);
            _bases.Add(newBase);
            newBase.PurchasedNewBase += CreateNewBase;
        }
    }
}
