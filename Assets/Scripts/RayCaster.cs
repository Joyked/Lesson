using System;
using UnityEngine;

[RequireComponent(typeof(ClickHandler))]
public class RayCaster : MonoBehaviour
{
    private ClickHandler _clickHandler;

    public event Action<RaycastHit> ObjectFound ; 
    
    private void Awake() =>
        _clickHandler = GetComponent<ClickHandler>();

    private void OnEnable() =>
        _clickHandler.MousePressed += ProcessRayHit;

    private void OnDisable() =>
        _clickHandler.MousePressed -= ProcessRayHit;

    private void ProcessRayHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
            ObjectFound?.Invoke(hit);
    }
}
