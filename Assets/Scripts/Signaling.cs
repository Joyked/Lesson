using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private float _speedVolume;
    
    private AudioSource _audioSource;
    private bool _rogueInHouse = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    private void Update()
    {
        if (_rogueInHouse)
        {
            _audioSource.volume += _speedVolume;
        }
        else
        {
            if(_audioSource.volume > 0)
                _audioSource.volume -= _speedVolume;
            else
                _audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rogue rogue))
        {
            _rogueInHouse = true;
            
            if (_audioSource.volume <= 0)
                _audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rogue rogue))
            _rogueInHouse = false;
    }
}
