using DG.Tweening;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    [SerializeField] private Vector3 _scale;
    [SerializeField] private float _duration;
    [SerializeField] private LoopType _loopType;

    private int _repeats = -1;
    
    private void Start() =>
        transform.DOScale(_scale, _duration).SetLoops(_repeats, _loopType);
}
