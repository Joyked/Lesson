using DG.Tweening;
using UnityEngine;

public class MoveTo : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private float _duration;
    [SerializeField] private LoopType _loopType;

    private int _repeats = -1;

    private void Start() =>
        transform.DOMove(_position, _duration).SetLoops(_repeats, _loopType);
}
