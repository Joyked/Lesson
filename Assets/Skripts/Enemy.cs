using System;
using UnityEngine;

public class Enemy : Mover
{
    public event Action<Enemy> ObjectKilled;

    [SerializeField] private ParticleSystem _particleSystem;
    private Hero _targetHero;
    
    public void SetDirection(Hero hero)
    {
        _targetHero = hero;
    }

    private void Update()
    {
        Direction = _targetHero.transform.position;
    }

    public void GetDamage()
    {
        float secontBeforeDestroy = 2f;
        var particle = Instantiate(_particleSystem, transform.position, transform.rotation);
        Destroy(particle, secontBeforeDestroy);
        ObjectKilled?.Invoke(this);
    }
}
