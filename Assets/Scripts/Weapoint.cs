using UnityEngine;

public class Weapoint : MonoBehaviour
{
    [SerializeField] private GameObject _particleSystem;
    
    private Animator _animator;
    private int Attack = Animator.StringToHash(nameof(Attack));
    
    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyMover enemy))
        {
            enemy.TakeDamage();
            _animator.SetBool(Attack, false);
        }
    }
}
