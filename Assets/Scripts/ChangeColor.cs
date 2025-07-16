using DG.Tweening;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private float _duration;
    [SerializeField] private LoopType _loopType;

    private int _repeats = -1;
    private Material _material;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
        _material.DOColor(_color, _duration).SetLoops(_repeats, _loopType);
    }
}