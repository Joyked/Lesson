using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class Mover: MonoBehaviour
{
    protected Vector3 Direction;
    protected Animator Animation;

    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [Space] 
    [SerializeField] private float _animationSpeed;
    [SerializeField] private float _speedCooficent;
    
    private NavMeshAgent _navMesh;
    private int Speed = Animator.StringToHash(nameof(Speed));
    
    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        Animation = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _navMesh.destination = Direction;

        if ((transform.position - Direction).sqrMagnitude / _speedCooficent > _maxSpeed)
            _navMesh.speed = _maxSpeed;
        else if ((transform.position - Direction).sqrMagnitude / _speedCooficent < _minSpeed && (transform.position - Direction).sqrMagnitude / _speedCooficent > 1)
            _navMesh.speed = _minSpeed;
        else if ((transform.position - Direction).sqrMagnitude / _speedCooficent < 1 && (transform.position - Direction).sqrMagnitude / _speedCooficent > 0.1)
            _navMesh.speed -=0.1f;
        else if ((transform.position - Direction).sqrMagnitude / _speedCooficent < 0.1)
            _navMesh.speed = 0;
        else
            _navMesh.speed = (transform.position - Direction).sqrMagnitude / _speedCooficent;
        
        Animation.SetFloat(Speed, _navMesh.speed);
        Animation.speed = _animationSpeed;
    }
}
