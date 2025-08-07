using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField] private AntBase _antBasePrefab;
    [SerializeField] private int _price;
    [Space] 
    [SerializeField] private QueueCookies _cookies;

    private AntSpawner _antSpawner;

    public void CreateNewBase(Flag flag, Counter counter)
    {
        if (counter.Count >= _price && flag.IsPosition)
        {
            var newBase = Instantiate(_antBasePrefab);
            newBase.Initialize(_cookies,  this);
            newBase.transform.position = flag.transform.position;
            newBase.AddAnt(counter.GetComponent<AntBase>().RemoveAnt());
            counter.GiveAway(_price);
            flag.ReturnPosition();
        }
    }
}
