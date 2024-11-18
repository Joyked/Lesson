using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    private CharacterController _controller;
    
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _controller.Move(direction * _speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _speedRotate * Time.deltaTime);
        }
    }
}
