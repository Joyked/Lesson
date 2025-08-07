using System.Collections.Generic;
using UnityEngine;

public class AntSpawner : MonoBehaviour
{
    [SerializeField] private Collector _antPrefab;
    [SerializeField] private int _price;
    
    public void SpawnNewAnt(AntBase antBase, Counter counter)
    {
        if (counter.Count >= _price)
        {
            var newAnt = Instantiate(_antPrefab);
            antBase.AddAnt(newAnt);
            counter.GiveAway(_price);
        }
    }
}
