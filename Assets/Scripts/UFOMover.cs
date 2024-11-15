using UnityEngine;

public class UFOMover : MonoBehaviour
{
    [SerializeField] private Transform[] _route;
    [SerializeField] private float _speed;

    private int _roadPoint;

    private void Update()
    {
        if (transform.position == _route[_roadPoint].position)
            _roadPoint = (_roadPoint + 1) % _route.Length;

        transform.position = Vector3.MoveTowards(transform.position, _route[_roadPoint].position, _speed * Time.deltaTime);
    }
}
