using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AntMover : MonoBehaviour
{
    private Collector _collector;
    private NavMeshAgent _navMesh;
    private bool _isPosition;

    public bool IsAvailable { get; private set; }  = true;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _collector = GetComponent<Collector>();
    }


    private void Update()
    {
        if (_isPosition == false && _collector.IsGetCookie == false)
            IsAvailable = true;
    }

    public void SetTarget(Transform target)
    {
        IsAvailable = false;
        
        _navMesh.SetDestination(target.position);
        _isPosition = true;
    }
    
    public void Strolle()
    {
        _collector.PutCookie();
        _isPosition = false;
        
        float minPosition = -5;
        float maxPosition = 5;
            
        float xPosition = Random.Range(minPosition, maxPosition);
        float zPosition = Random.Range(minPosition, maxPosition);
            
        Vector3 freePosition = new Vector3(xPosition, transform.position.y, zPosition);
        _navMesh.SetDestination(freePosition);
    }
}
