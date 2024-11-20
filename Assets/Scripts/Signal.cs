using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signal : MonoBehaviour
{
    [SerializeField] private float _speedVolume;
    
    private AudioSource _audioSource;
    private bool _isDetected;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    private IEnumerator SoundEffect()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
        _audioSource.Play();
        
        while (_isDetected || _audioSource.volume > 0)
        {
            if (_isDetected)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 1, _speedVolume);
            }
            else if (_audioSource.volume > 0)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 0, _speedVolume);
            }
            else
            {
                _audioSource.Stop();
                StopCoroutine(SoundEffect());
            }
            
            yield return waitForSeconds;
        }
    }

    public void TurnOn()
    {
        _isDetected = true;

        if (_audioSource.volume <= 0)
            StartCoroutine(SoundEffect());
    }

    public void TurnOff()
    {
        _isDetected = false;
    }
}
