using UnityEngine;

public class Cookie : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    private void OnEnable() =>
        _rigidbody.velocity = Vector3.zero;
    
    
}