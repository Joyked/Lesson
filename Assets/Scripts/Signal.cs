using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signal : MonoBehaviour
{
    [SerializeField] private Detector _detector;
    [SerializeField] private float _speedVolume;
    
    private AudioSource _audioSource;
    private bool _isDetected;
    private float _maxVolume = 1;
    private float _minVolume = 0f;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    private void OnEnable()
    {
        _detector.RogueIsHere += TurnOn;
        _detector.RogueIsntHere += TurnOff;
    }

    private void OnDisable()
    {
        _detector.RogueIsHere -= TurnOn;
        _detector.RogueIsntHere -= TurnOff;
    }

    private IEnumerator SoundEffect()
    {
        float cycleTime = 0.1f;
        WaitForSeconds waitForSeconds = new WaitForSeconds(cycleTime);
        _audioSource.Play();
        
        while (_isDetected || _audioSource.volume > 0)
        {
            if (_isDetected)
            {
                ChangeVolume(_maxVolume);
            }
            else if (_audioSource.volume > _minVolume)
            {
                ChangeVolume(_minVolume);
            }
            else
            {
                _audioSource.Stop();
                StopCoroutine(SoundEffect());
            }
            
            yield return waitForSeconds;
        }
    }

    private void ChangeVolume(float requiredVolume)
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, requiredVolume, _speedVolume);
    }
    
    private void TurnOn()
    {
        _isDetected = true;

        if (_audioSource.volume <= _minVolume)
            StartCoroutine(SoundEffect());
    }

    private void TurnOff()
    {
        _isDetected = false;
    }
}
