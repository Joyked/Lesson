using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private UFOMover _target;
    private float _speed;

    public Action<Projectile> HitedTarget;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }

    public void SetParams(UFOMover target, float speed)
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
