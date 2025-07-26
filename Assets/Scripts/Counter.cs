using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    [SerializeField] private Base _base;

    private Text _text;
    private int _count;

    private void Awake() =>
        _text = GetComponent<Text>();

    private void OnEnable() =>
        _base.CookieOnBase += Increase;

    private void OnDisable() =>
        _base.CookieOnBase -= Increase;

    private void Increase(Cookie cookie)
    {
        _count++;
        _text.text = _count.ToString();
    }
}
