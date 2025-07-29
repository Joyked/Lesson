using System;
using UnityEngine;

public class CreateBase : MonoBehaviour
{
    [SerializeField] private Flag _flag;
    [SerializeField] private int _priceNewBase = 5;
    [SerializeField] private CookieSpawner _cookieSpawner;

    private BaseAnt _baseAnt;
    private Counter _counter;
    private bool _isEnoughResources;

    public bool IsPosition { get; private set; }

    private void Awake()
    {
        _baseAnt = GetComponent<BaseAnt>();
        _counter = GetComponent<Counter>();
    }

    private void Start() =>
        ReturnPosition();

    private void Update() =>
        Build();

    public void NewPosition(Vector3 newPosition)
    {
        _flag.transform.position = newPosition;
        IsPosition = true;
    }

    public void ReturnPosition()
    {
        _flag.transform.position = transform.position;
        IsPosition = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out BaseAnt baseAnt))
            IsPosition = false;
    }

    private void Build()
    {
        if (_counter.Count >= _priceNewBase && IsPosition && _baseAnt.AntCount > 1)
        {
            CreateBase newBase = Instantiate(this);
            newBase.transform.position = _flag.transform.position;
            _counter.BuyBase(_priceNewBase);
            BaseAnt baseAnt = newBase.GetComponent<BaseAnt>();

            for (int i = 0; i < baseAnt.AntCount; i++)
                baseAnt.RemoveAnt();
            
            _cookieSpawner.AddNewBase(baseAnt);
            baseAnt.Create(_baseAnt.RemoveAnt());
            ReturnPosition();
        }
    }
}
