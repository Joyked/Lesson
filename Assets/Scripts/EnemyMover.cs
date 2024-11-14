using System;
using UnityEngine;

public class EnemyMover : Mover
{
    [SerializeField] private GameObject _particleSystem;
    
    private HeroMover _targetHeroMover;
    
    public event Action<EnemyMover> Killed;
    
    private void Update()
    {
        Direction = _targetHeroMover.transform.position;
    }

    public void SetDirection(HeroMover heroMover)
    {
        _targetHeroMover = heroMover;
    }
    
    public void TakeDamage()
    {
        Killed?.Invoke(this);
        float secontBeforeDestroy = 2f;
        var particle = Instantiate(_particleSystem, transform.position, transform.rotation);
        Destroy(particle, secontBeforeDestroy);
    }
}
