using System;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private Vector3 _basePosition;
    
    public bool IsPosition { get; private set; }

    private void Awake() =>
        _basePosition = transform.position;

    public void NewPosition(Vector3 position)
    {
        transform.position = position;
        IsPosition = true;
    }

    public void ReturnPosition()
    {
        transform.position = _basePosition;
        IsPosition = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AntBase antBase))
            IsPosition = false;
    }
}
