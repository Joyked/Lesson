using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
    [SerializeField] private string _newText;
    [SerializeField] private float _duration;
    [SerializeField] private LoopType _loopType;
    [SerializeField] private float _delay;

    private int _repeats = -1;
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        _text.DOText(_newText, _duration);
        _text.DOText(" Дополнение к тексту", _duration).SetDelay(_delay).SetRelative();
        _text.DOText("Взлом текста", _duration, true, ScrambleMode.All).SetDelay(_delay * 3);
    }
}
