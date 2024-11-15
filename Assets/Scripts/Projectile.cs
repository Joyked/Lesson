using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _target;
    private float _speed;

    public Action<Projectile> HitedTarget;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public void SetParams(Transform target, float speed)
    {
        _target = target;
        _speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out UFOMover ufoMover))
            HitedTarget?.Invoke(this);
    }
}
