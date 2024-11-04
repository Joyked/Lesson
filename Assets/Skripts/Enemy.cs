using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _powerAttraction;

    private CharacterController _controller;

    public event Action<Enemy> ObjectFelled;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _controller.Move(transform.forward * _speed);
        
        if (_controller.isGrounded == false)
            _controller.Move(transform.up * -1 * _powerAttraction);
        
        if (transform.position.y < -5)
            ObjectFelled?.Invoke(this);
    }

    public void SetDirection(Vector3 direction)
    {
        transform.transform.forward += direction;
    }
}
