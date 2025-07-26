using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AntMover : MonoBehaviour
{
    [SerializeField] private Transform _mandible;
    [SerializeField] private Base _base;
    
    private NavMeshAgent _navMesh;
    private Cookie _cookieTarget;
    private bool _isGetCookie;
    private bool _isPosition;

    public bool IsAvailable { get; private set; }  = true;

    private void Awake() =>
        _navMesh = GetComponent<NavMeshAgent>();


    private void Update()
    {
        if (_isGetCookie)
            _cookieTarget.transform.position = _mandible.position;

        if (_isPosition == false && _isGetCookie == false)
            IsAvailable = true;
    }

    public void SetTarget(Cookie cookie)
    {
        IsAvailable = false;
        _cookieTarget = cookie;
        _navMesh.SetDestination(_cookieTarget.transform.position);
        _isPosition = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cookie cookie) && _isGetCookie == false)
        {
            if (cookie == _cookieTarget)
            {
                _isGetCookie = true;
                Collider collider = cookie.GetComponent<Collider>();
                collider.isTrigger = true;
                _navMesh.SetDestination(_base.transform.position);
                _isPosition = false;
            }
        }

        if (other.gameObject.TryGetComponent(out Base anthill))
        {
            _isGetCookie = false;
            _cookieTarget = null;
            
            float xPosition = Random.Range(-5, 5);
            float zPosition = Random.Range(-5, 5);
            
            Vector3 freePosition = new Vector3(xPosition, transform.position.y, zPosition);
            _navMesh.SetDestination(freePosition);
        }
    }
}
