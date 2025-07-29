using System;
using UnityEngine;
using UnityEngine.UI;

public class CookieView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    
     private Text _text;

     private void Awake() =>
         _text = GetComponent<Text>();

     private void OnEnable() =>
        _counter.Changed += Draw;

    private void OnDisable() =>
        _counter.Changed -= Draw;

    private void Draw(int count) =>
        _text.text = count.ToString();
}
