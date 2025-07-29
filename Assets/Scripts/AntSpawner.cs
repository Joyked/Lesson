using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    [SerializeField] private AntMover _antPrefab;
    [SerializeField] private int _priceNewAnt = 3;
    
    private BaseAnt _baseAnt;
    private Counter _counter;
    private CreateBase _createBase;

    private void Awake()
    {
        _counter = GetComponent<Counter>();
        _baseAnt = GetComponent<BaseAnt>();
        _createBase = GetComponent<CreateBase>();
    }

    private void OnEnable() =>
        _counter.Changed += CreateAnt;

    private void OnDisable() =>
        _counter.Changed -= CreateAnt;

    private void CreateAnt(int cookieCount)
    {
        if (cookieCount >= _priceNewAnt && _createBase.IsPosition == false)
        {
            _counter.BuyAnt(_priceNewAnt);
            AntMover ant = Instantiate(_antPrefab);
            _baseAnt.AddAnt(ant.GetComponent<Collector>());
            
            if(ant.IsAvailable)
                ant.Strolle();
        }
    }
}
