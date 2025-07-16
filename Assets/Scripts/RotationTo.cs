using DG.Tweening;
using UnityEngine;

public class RotationTo : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _duration;
    [SerializeField] private LoopType _loopType;

    private int _repeats = -1;
    
    private void Start() =>
        transform.DORotate(_rotation, _duration).SetLoops(_repeats, _loopType);
}
