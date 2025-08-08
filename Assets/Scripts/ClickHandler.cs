using System;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public event Action MousePressed;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MousePressed?.Invoke();
    }
}
