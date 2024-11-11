using UnityEngine;

public class Weapoint : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.GetDamage();
            _animator.SetBool("Attack", false);
        }
    }
}
