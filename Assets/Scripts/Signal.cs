using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Signal : MonoBehaviour
{
    [SerializeField] private Detecor _detecor;
    [SerializeField] private float _speedVolume;
    
    private AudioSource _audioSource;
    private bool _isDetected;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }
    
    private void Start()
    {
        StartCoroutine(SoundEffect());
    }
    
    private void OnEnable()
    {
        _detecor.RogueInHouse += ReactToRogue;
    }

    private void OnDisable()
    {
        _detecor.RogueInHouse -= ReactToRogue;
    }

    private IEnumerator SoundEffect()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

        while (true)
        {
            if (_isDetected)
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
            
            yield return waitForSeconds;
        }
    }

    private void ReactToRogue(bool isDetected)
    {
        _isDetected = isDetected;

        if (_isDetected && _audioSource.volume == 0)
            _audioSource.Play();
    }
}
